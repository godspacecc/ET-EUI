using System;
using System.Threading.Tasks;

namespace ET
{
    public class G2M_RequsetEnterGameStateHandler: AMActorLocationRpcHandler<Unit,G2M_RequsetEnterGameState, M2G_RequsetEnterGameState>
    {
        protected override async ETTask Run(Unit unit, G2M_RequsetEnterGameState request, M2G_RequsetEnterGameState response, Action reply)
        {
            Log.Debug("开始下线保存玩家数据");
            unit.GetComponent<UnitDBSaveComponent>()?.SaveChange();
            
            reply();
            
            //正式释放Unit
            await unit.RemoveLocation();
            UnitComponent unitComponent = unit.DomainScene().GetComponent<UnitComponent>();
            unitComponent.Remove(unit.Id);
            
            await ETTask.CompletedTask;
        }
    }
}