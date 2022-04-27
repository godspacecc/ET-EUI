using System.Collections.Generic;

namespace ET
{
    [ChildType(typeof(ServerInfo))]
    public class ServerInfosComponent:Entity,IAwake,IDestroy
    {
        public List<ServerInfo> ServerInfoList = new List<ServerInfo>();

        public int CurrentServerdId = 0;
    }
}