﻿namespace ET
{
    public class AccountInfoComponent:Entity,IAwake,IDestroy
    {
        public string Token;
        public long AccountId;
        public string RelamKey;
        public string RelamAddress;
    }
}