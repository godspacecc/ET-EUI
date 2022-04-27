using System;

namespace ET
{
    [Timer(TimerType.AccountSessionCheckOutTimer)]
    public class AccountSessionCheckOutTimer: ATimer<AccountCheckOutTimeComponent>
    {
        public override void Run(AccountCheckOutTimeComponent t)
        {
            try
            {
                t.DeletedSession();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }
    
    
    public class AccountCheckOutTimeComponentAwakeSystem:AwakeSystem<AccountCheckOutTimeComponent,long>
    {
        public override void Awake(AccountCheckOutTimeComponent self, long accountId)
        {
            self.AccountId = accountId;
            TimerComponent.Instance.Remove(ref self.Timer);
            self.Timer = TimerComponent.Instance.NewOnceTimer(TimeHelper.ServerNow()+600000, TimerType.AccountSessionCheckOutTimer, self);
        }
    }
    
    public class AccountCheckOutTimeComponentDestorySystem:DestroySystem<AccountCheckOutTimeComponent>
    {
        public override void Destroy(AccountCheckOutTimeComponent self)
        {
            self.AccountId = 0;
            TimerComponent.Instance.Remove(ref self.Timer);
        }
    }
    [FriendClass(typeof(AccountCheckOutTimeComponent))]
    public static class AccountCheckOutTimeComponentSystem
    {
        public static void DeletedSession(this AccountCheckOutTimeComponent self)
        {
            Session session = self.GetParent<Session>();
            long sessionInstanceId = session.DomainScene().GetComponent<AccountSessionsComponent>().Get(self.AccountId);
            if (session.InstanceId == sessionInstanceId)
            {
                session.DomainScene().GetComponent<AccountSessionsComponent>().Remove(self.AccountId);
            }
            session?.Send(new A2C_Disconnected(){ Error = 1 });
            session?.DisConnect().Coroutine();
        }
    }
}