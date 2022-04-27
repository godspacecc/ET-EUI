using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ET
{
    [FriendClass(typeof(Account))]
    public class C2A_LoginAccountHandler:AMRpcHandler<C2A_LoginAccount, A2C_LoginAccount>
    {
        protected override async ETTask Run(Session session, C2A_LoginAccount request, A2C_LoginAccount response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Account)
            {
                Log.Debug($"请求的SceneType错误,当前的Scene为{session.DomainScene().SceneType}");
                session.Dispose();
                return;
            }
            
            session.RemoveComponent<SessionAcceptTimeoutComponent>();
            
            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                reply();
                session.DisConnect().Coroutine();
                return;
            }
            
            if (string.IsNullOrEmpty(request.Account) || string.IsNullOrEmpty(request.Password))
            {
                response.Error = ErrorCode.ERR_LoginInfoIsNullError;
                reply();
                session.DisConnect().Coroutine();
                return;
            }

            if (!Regex.IsMatch(request.Account.Trim(), @"^(?=.*[0-9].*)(?=.*[A-Z].*)(?=.*[a-z].*).{6,15}$"))
            {
                response.Error = ErrorCode.ERR_AccountFormatError;
                reply();
                session.DisConnect().Coroutine();
                return;
            }
            
            if (!Regex.IsMatch(request.Password.Trim(), @"^[A-Za-z0-9]+$"))
            {
                response.Error = ErrorCode.ERR_PasswordFormatError;
                reply();
                session.DisConnect().Coroutine();
                return;
            }

            using (session.AddComponent<SessionLockingComponent>())
            {
                //防止同时注册 通过锁住账户(唯一)来实现 同账户不可同时执行注册操作
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginAccount, request.Account.Trim().GetHashCode()))
                {
                    var accountList = await DBManagerComponent.Instance.GetZoneDB(session.DomainZone())
                            .Query<Account>(a => a.AccountName.Equals(request.Account.Trim()));

                    Account account = null;
                    if (accountList !=null && accountList.Count > 0)
                    {
                        account = accountList[0];
                        session.AddChild(account);
                        if (account.AccountType == (int)AccountType.BlackList)
                        {
                            response.Error = ErrorCode.ERR_AccountInfoBlackListError;
                            reply();
                            session.DisConnect().Coroutine();
                            account?.Dispose();
                            return;
                        }

                        if (!account.Password.Equals(request.Password))
                        {
                            response.Error = ErrorCode.ERR_PasswordError;
                            reply();
                            session.DisConnect().Coroutine();
                            account?.Dispose();
                            return;
                        }
                    }
                    else
                    {
                        account = session.AddChild<Account>();
                        account.AccountName = request.Account.Trim();
                        account.Password = request.Password;
                        account.CreateTime = TimeHelper.ServerNow();
                        account.AccountType = (int)AccountType.General;
                        await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Save<Account>(account);
                    }

                    StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.DomainZone(), "LoginCenter");
                    long LoginCenterSessionId = startSceneConfig.InstanceId;
                    var loginAccountResponse = 
                            (L2A_LoginAccountResponse) await ActorMessageSenderComponent.Instance.Call
                                    (LoginCenterSessionId, new A2L_LoginAccountRequest(){ AccountId = account.Id});

                    if (loginAccountResponse.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = loginAccountResponse.Error;
                        reply();
                        session.DisConnect().Coroutine();
                        account?.Dispose();
                        return;
                    }
                    
                    long accountsessionInstanceId = session.DomainScene().GetComponent<AccountSessionsComponent>().Get(account.Id);
                    Session otherSession = Game.EventSystem.Get(accountsessionInstanceId) as Session;
                    if (otherSession !=null)
                    {
                        otherSession.Send(new A2C_Disconnected(){ Error = 0});
                        otherSession.DisConnect().Coroutine();
                    }
                    session.DomainScene().GetComponent<AccountSessionsComponent>().Add(account.Id, session.InstanceId);
                    session.AddComponent<AccountCheckOutTimeComponent, long>(account.Id);
                    
                    string token = TimeHelper.ServerNow().ToString() + RandomHelper.RandomNumber(int.MinValue, int.MaxValue);
                    session.DomainScene().GetComponent<TokenComponent>().Remove(account.Id);
                    session.DomainScene().GetComponent<TokenComponent>().Add(account.Id, token);
            
                    response.Token = token;
                    response.AccountId = account.Id;
            
                    reply();
                    account?.Dispose();
                }
            }
            await Task.CompletedTask;
        }
    }
}