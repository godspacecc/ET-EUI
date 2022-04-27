using System;

namespace ET
{
    [FriendClass(typeof(RoleInfo))]
    public class C2A_DeleteRoleHandler: AMRpcHandler<C2A_DeleteRole, A2C_DeleteRole>
    {
        protected override async ETTask Run(Session session, C2A_DeleteRole request, A2C_DeleteRole response, Action reply)
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
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.CreateRole, request.AccountId))
                {
                    var roleInfos = await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).
                            Query<RoleInfo>(d=> d.ServerId == request.ServerId && d.Id == request.RoleInfoId);

                    if (roleInfos == null && roleInfos.Count == 0)
                    {
                        response.Error = ErrorCode.ERR_RoleNoExist;
                        reply();
                        return;
                    }

                    var roleinfo = roleInfos[0];
                    session.AddChild(roleinfo);
                    
                    roleinfo.State = (int)RoleInfoState.Freeze;

                    await DBManagerComponent.Instance.GetZoneDB(request.ServerId).Save(roleinfo);
                    response.DeleteRoleInfoId = roleinfo.Id;
                    roleinfo?.Dispose();
                    reply();
                }
            }
        }
    }
}