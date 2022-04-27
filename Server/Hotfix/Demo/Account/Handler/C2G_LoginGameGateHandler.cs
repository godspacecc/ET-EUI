using System;

namespace ET
{
    [FriendClass(typeof(SessionPlayerComponent))]
    [FriendClass(typeof(SessionStateComponent))]
    public class C2G_LoginGameGateHandler: AMRpcHandler<C2G_LoginGameGate, G2C_LoginGameGate>
    {
        protected override async ETTask Run(Session session, C2G_LoginGameGate request, G2C_LoginGameGate response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Gate)
            {
                Log.Debug($"请求的Scene错误,当前的Scene为{session.DomainScene().SceneType}");
                session.Dispose();
                return;
            }
            
            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                reply();
                return;
            }

            Scene domainscene = session.DomainScene();
            string token = domainscene.GetComponent<GateSessionKeyComponent>().Get(request.Account);

            if (token == null || token != request.Key)
            {
                response.Error = ErrorCode.ERR_GateTokenError;
                response.Message = "Gate Key 验证失败!";
                reply();
                session?.DisConnect().Coroutine();
                return;
            }
            
            domainscene.GetComponent<GateSessionKeyComponent>().Remove(request.Account);

            long instanceid = session.InstanceId;
            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate, request.Account))
                {
                    if (instanceid != session.InstanceId)
                    {
                        return;
                    }

                    StartSceneConfig config = StartSceneConfigCategory.Instance.LoginCenterConfig;
                    L2G_AddLoginRecord l2GAddLoginRecord = (L2G_AddLoginRecord)await MessageHelper.CallActor(config.InstanceId,
                        new G2L_AddLoginRecord() { AccountId = request.Account, ServerId = domainscene.Zone });

                    if (l2GAddLoginRecord.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = l2GAddLoginRecord.Error;
                        reply();
                        session?.DisConnect().Coroutine();
                        return;
                    }

                    SessionStateComponent sessionStateComponent = session.GetComponent<SessionStateComponent>();
                    if (sessionStateComponent == null)
                    {
                        sessionStateComponent =  session.AddComponent<SessionStateComponent>();
                    }

                    sessionStateComponent.State = SessionState.Normal;

                    Player player = domainscene.GetComponent<PlayerComponent>().Get(request.Account);

                    if (player == null)
                    {
                        player = domainscene.GetComponent<PlayerComponent>().AddChildWithId<Player,long,long>(request.RoleId, request.Account, request.RoleId);
                        player.PlayerState = PlayerState.Gate;
                        domainscene.GetComponent<PlayerComponent>().Add(player);
                        session.AddComponent<MailBoxComponent, MailboxType>(MailboxType.GateSession);
                    }
                    else
                    {
                        player.RemoveComponent<PlayerOffineOutTimeComponent>();
                    }

                    session.AddComponent<SessionPlayerComponent>().PlayerId = player.Id;
                    session.GetComponent<SessionPlayerComponent>().PlayerInstanceId = player.InstanceId;
                    session.GetComponent<SessionPlayerComponent>().AccountId = request.Account;
                    player.ClientSession = session;
                    response.PlayerId = player.Id; //待定
                }
                reply();
            }
        }
    }
}