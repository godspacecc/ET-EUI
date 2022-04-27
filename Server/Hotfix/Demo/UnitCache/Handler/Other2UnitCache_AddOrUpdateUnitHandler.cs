using System;

namespace ET
{
    public class Other2UnitCache_AddOrUpdateUnitHandler: AMActorRpcHandler<Scene, Other2UnitCache_AddOrUpdateUnit, UnitCache2Other_AddOrUpdateUnit>
    {
        protected override async ETTask Run(Scene scene, Other2UnitCache_AddOrUpdateUnit request, UnitCache2Other_AddOrUpdateUnit response, Action reply)
        {
            UpdateUnitCacheAsync(scene, request, response).Coroutine();
            reply();
            await ETTask.CompletedTask;
        }

        private async ETTask UpdateUnitCacheAsync(Scene scene, Other2UnitCache_AddOrUpdateUnit request, UnitCache2Other_AddOrUpdateUnit respone)
        {
            UnitCacheComponent unitCacheComponent = scene.GetComponent<UnitCacheComponent>();
            using (ListComponent<Entity> entityList = new ListComponent<Entity>())
            {
                for (int index = 0; index < request.EntityType.Count; index++)
                {
                    Type type = Game.EventSystem.GetType(request.EntityType[index]);
                    Entity entity = (Entity)MongoHelper.FromBson(type, request.EntityBytes[index]);
                    entityList.Add(entity);
                }

                await unitCacheComponent.AddOrUpdate(request.UnitId, entityList);
            }
        }
    }
}