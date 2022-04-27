using System;

namespace ET
{
    public class C2A_GetRelamKeyHandler: AMRpcHandler<C2A_GetRelamKey, A2C_GetRelamKey>
    {
        protected override async ETTask Run(Session session, C2A_GetRelamKey request, A2C_GetRelamKey response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Account)
            {
                Log.Debug($"请求的SceneType错误,当前的Scene为{session.DomainScene().SceneType}");
                session.Dispose();
                return;
            }

            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                reply();
                session.DisConnect().Coroutine();
                return;
            }
            
            string token = session.DomainScene().GetComponent<TokenComponent>().Get(request.AccountId);

            if (token == null || token != request.Token)
            {
                response.Error = ErrorCode.ERR_TokenError;
                reply();
                return;
            }
            
            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginAccount, request.AccountId))
                {
                    StartSceneConfig relamstartSceneConfig = RealmGateAddressHelper.GetRelam(request.ServerId);
                    //relamstartSceneConfig.Id
                    R2A_GetRelamKey r2AGetRelamKey = (R2A_GetRelamKey)await MessageHelper.CallActor(relamstartSceneConfig.InstanceId, 
                        new A2R_GetRelamKey() { AccountId = request.AccountId });
                    if (r2AGetRelamKey.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = r2AGetRelamKey.Error;
                        reply();
                        session?.DisConnect().Coroutine();
                        return;
                    }

                    response.RelamKey = r2AGetRelamKey.RelamKey;
                    response.RelamAddress = relamstartSceneConfig.OuterIPPort.ToString();
                    reply();
                    session?.DisConnect().Coroutine();
                }
            }

            await ETTask.CompletedTask;
        }
    }
}