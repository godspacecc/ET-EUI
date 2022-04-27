namespace ET
{
    public class AccountSessionsComponentDestorySystem:DestroySystem<AccountSessionsComponent>
    {
        public override void Destroy(AccountSessionsComponent self)
        {
            self.AccountSessionsDictionary.Clear();
        }
    }
    [FriendClass(typeof(AccountSessionsComponent))]
    public static class AccountSessionsComponentSystem
    {
        public static long Get(this AccountSessionsComponent self, long account)
        {
            if (!self.AccountSessionsDictionary.TryGetValue(account, out long InstanceId))
            {
                return 0;
            }

            return InstanceId;
        }
        
        public static void Remove(this AccountSessionsComponent self, long account)
        {
            if (self.AccountSessionsDictionary.ContainsKey(account))
            {
                self.AccountSessionsDictionary.Remove(account);
            }
        }
        
        public static void Add(this AccountSessionsComponent self, long account, long sessionInstanceId)
        {
            if (self.AccountSessionsDictionary.ContainsKey(account))
            {
                self.AccountSessionsDictionary[account] = sessionInstanceId;
            }
        }
    }
}