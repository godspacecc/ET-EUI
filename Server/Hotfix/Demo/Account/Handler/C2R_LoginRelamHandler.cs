using System;

namespace ET
{
    public class C2R_LoginRelamHandler:AMRpcHandler<C2R_LoginRelam, R2C_LoginRelam>
    {
        protected override async ETTask Run(Session session, C2R_LoginRelam request, R2C_LoginRelam response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Realm)
            {
                Log.Debug($"请求的Scene错误,当前的Scene为{session.DomainScene().SceneType}");
                session.Dispose();
                return;
            }

            Scene domainscene = session.DomainScene();

            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                reply();
                session.DisConnect().Coroutine();
                return;
            }
            
            string token = session.DomainScene().GetComponent<TokenComponent>().Get(request.AccountId);

            if (token == null || token != request.RelamTokenKey)
            {
                response.Error = ErrorCode.ERR_TokenError;
                reply();
                return;
            }
            
            domainscene.GetComponent<TokenComponent>().Remove(request.AccountId);
            
            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginRelam, request.AccountId))
                {
                    StartSceneConfig config = RealmGateAddressHelper.GetGate(domainscene.Zone, request.AccountId);
                    G2R_GetLoginGateKey g2RGetLoginKey =
                            (G2R_GetLoginGateKey)await MessageHelper.CallActor(config.InstanceId, new R2G_GetLoginGateKey()
                            {
                                AccountId = request.AccountId,
                            });
                    if (g2RGetLoginKey.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = g2RGetLoginKey.Error;
                        reply();
                        return;
                    }

                    response.GateSessionKey = g2RGetLoginKey.GateSessionKey;
                    response.GateAddress = config.OuterIPPort.ToString();
                    reply();

                    session?.DisConnect().Coroutine();
                }
            }
        }
    }
}