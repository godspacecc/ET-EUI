using System;

namespace ET
{
    public class M2G_RequsetExitGameHandler: AMActorLocationRpcHandler<Unit,G2M_RequsetExitGame,M2G_RequsetExitGame>
    {
        protected override async ETTask Run(Unit unit, G2M_RequsetExitGame request, M2G_RequsetExitGame response, Action reply)
        {
            //保存玩家数据到数据库 TODO
            reply();

            await unit.RemoveLocation();
            UnitComponent unitComponent = unit.DomainScene().GetComponent<UnitComponent>();
            unitComponent.Remove(unit.Id);
            
            await ETTask.CompletedTask;
        }
    }
}