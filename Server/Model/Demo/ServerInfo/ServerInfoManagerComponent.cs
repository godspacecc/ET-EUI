using System.Collections.Generic;

namespace ET
{
    [ChildType(typeof(ServerInfo))]
    public class ServerInfoManagerComponent:Entity,IAwake,IDestroy,ILoad
    {
        public List<ServerInfo> ServerInfos = new List<ServerInfo>();
    }
}