using UnityEngine;

namespace ET
{
    [FriendClass(typeof(GlobalComponent))]
    public class AfterUnitCreate_CreateUnitView: AEvent<EventType.AfterUnitCreate>
    {
        protected override async void Run(EventType.AfterUnitCreate args)
        {
            // Unit View层
            // 这里可以改成异步加载，demo就不搞了
            // GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset("Unit.unity3d", "Unit");
            // GameObject prefab = bundleGameObject.Get<GameObject>("Hero");
	           //
            // GameObject go = UnityEngine.Object.Instantiate(prefab, GlobalComponent.Instance.Unit, true);
            // go.transform.position = args.Unit.Position;
            // args.Unit.AddComponent<GameObjectComponent>().GameObject = go;
            // args.Unit.AddComponent<AnimatorComponent>();
            // Game.Scene.GetComponent<CameraComponent>().Lock(args.Unit);
            
            await ResourcesComponent.Instance.LoadBundleAsync("Kinght.unity3d");
            GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset("Kinght.unity3d", "Kinght");
            GameObject go = UnityEngine.Object.Instantiate(bundleGameObject);
            go.transform.SetParent(GlobalComponent.Instance.Unit, true); //所有unit都挂在这个下面
            
            args.Unit.AddComponent<GameObjectComponent>().GameObject = go;
            args.Unit.AddComponent<AnimatorComponent>();
            
            args.Unit.Position = Vector3.zero;
            await ETTask.CompletedTask;
        }
    }
}