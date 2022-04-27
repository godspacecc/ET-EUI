namespace ET
{
    public class RoleInfosComponentDestorySytem: DestroySystem<RoleInfosComponent>
    {
        public override void Destroy(RoleInfosComponent self)
        {
            foreach (var roleInfo in self.RoleInfos)
            {
                roleInfo?.Dispose();
            }
            self.RoleInfos.Clear();
            self.CurrentRoleId = 0;
        }
    }
    [FriendClass(typeof(RoleInfosComponent))]
    public static class RoleInfosComponentSystem
    {
        public static void Add(this RoleInfosComponent self, RoleInfo roleInfo)
        { 
            self.RoleInfos.Add(roleInfo); 
        }
    }
}