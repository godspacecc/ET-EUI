using System;
using System.Collections.Generic;

namespace ET
{
    public class UnitDBSaveComponent: Entity, IAwake, IDestroy
    {
        public HashSet<Type> EntityChangeTypeSet = new HashSet<Type>();

        public long Timer;
    }
}