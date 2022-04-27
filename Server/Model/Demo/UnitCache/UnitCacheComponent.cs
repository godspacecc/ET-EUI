using System.Collections.Generic;

namespace ET
{
    [ChildType(typeof(UnitCache))]
    public class UnitCacheComponent: Entity, IAwake, IDestroy
    {
        public Dictionary<string, UnitCache> UnitCaches = new Dictionary<string, UnitCache>();

        public List<string> UnitCacheKeyList = new List<string>();
    }
}