namespace ET
{
    [FriendClass(typeof(ServerInfo))]
    public static class ServerInfoSystem
    {
        public static void FromMessage(this ServerInfo self, ServerInfoProto serverInfoProto)
        {
            self.Id = serverInfoProto.Id;
            self.ServerName = serverInfoProto.ServerName;
            self.Status = serverInfoProto.Status;
        }
        
        public static ServerInfoProto ToMessage(this ServerInfo self)
        {
            return new ServerInfoProto() { Id = (int)self.Id, Status = self.Status, ServerName = self.ServerName };
        }
    }
}