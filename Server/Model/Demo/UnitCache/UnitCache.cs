using System.Collections.Generic;

namespace ET
{
    public interface IUnitCache
    {
        
    }

    public class UnitCache: Entity, IAwake, IDestroy
    {
        public string key;

        public Dictionary<long, Entity> CacheComponentsDictionary = new Dictionary<long, Entity>();
    }
}