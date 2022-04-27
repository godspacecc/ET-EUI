using System;

namespace ET
{
    [FriendClass(typeof(RoleInfo))]
    public class C2A_CreateRoleHandler: AMRpcHandler<C2A_CreateRole,A2C_CreateRole>
    {
        protected override async ETTask Run(Session session, C2A_CreateRole request, A2C_CreateRole response, Action reply)
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
            
            if (string.IsNullOrEmpty(request.Name))
            {
                response.Error = ErrorCode.ERR_RoleNameIsNullError;
                reply();
                return;
            }

            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.CreateRole, request.AccountId))
                {
                    var accountInfos = await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).
                            Query<RoleInfo>(d=>d.Name == request.Name && d.ServerId == request.ServerId);

                    if (accountInfos != null && accountInfos.Count > 0)
                    {
                        response.Error = ErrorCode.ERR_RoleNameSame;
                        reply();
                        return;
                    }

                    RoleInfo roleInfo = session.AddChildWithId<RoleInfo>(IdGenerater.Instance.GenerateUnitId(request.ServerId));
                    roleInfo.Name = request.Name;
                    roleInfo.State = (int)RoleInfoState.Normal;
                    roleInfo.ServerId = 1;
                    roleInfo.AccountId = request.AccountId;
                    roleInfo.CreateTime = TimeHelper.ServerNow();
                    roleInfo.LastLoginTime = 0;

                    await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Save<RoleInfo>(roleInfo);

                    response.RoleInfo = roleInfo.ToMessage();

                    reply();
                
                    roleInfo?.Dispose();
                }
            }
        }
    }
}