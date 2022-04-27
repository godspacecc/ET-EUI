namespace ET
{
    public class LoginInfoRecordComponentDestroySystem:DestroySystem<LoginInfoRecordComponent>
    {
        public override void Destroy(LoginInfoRecordComponent self)
        {
            self.AccountLoginInfoDictionary.Clear();
        }
    }
    [FriendClass(typeof(LoginInfoRecordComponent))]
    public static class LoginInfoRecordComponentSystem
    {
        public static void Add(this LoginInfoRecordComponent self, long key, int value)
        {
            if (self.AccountLoginInfoDictionary.ContainsKey(key))
            {
                self.AccountLoginInfoDictionary[key] = value;
                return;
            }
            self.AccountLoginInfoDictionary.Add(key, value);
        }
        
        public static void Remove(this LoginInfoRecordComponent self, long key)
        {
            if (self.AccountLoginInfoDictionary.ContainsKey(key))
            {
                self.AccountLoginInfoDictionary.Remove(key);
            }
        }
        
        public static int Get(this LoginInfoRecordComponent self, long key)
        {
            if (!self.AccountLoginInfoDictionary.TryGetValue(key, out int value))
            {
                return -1;
            }

            return value;
        }
        
        public static bool IsExist(this LoginInfoRecordComponent self, long key)
        {
            return self.AccountLoginInfoDictionary.ContainsKey(key);
        }
    }
}