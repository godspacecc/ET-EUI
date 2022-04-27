using System;

namespace ET
{
    public class A2R_GetRelamKeyHandler: AMActorRpcHandler<Scene, A2R_GetRelamKey, R2A_GetRelamKey>
    {
        protected override async ETTask Run(Scene scene, A2R_GetRelamKey request, R2A_GetRelamKey response, Action reply)
        {
            if (scene.DomainScene().SceneType != SceneType.Realm)
            {
                Log.Debug($"请求的Scene错误,当前的Scene为{scene.DomainScene().SceneType}");
                response.Error = ErrorCode.ERR_RequestSceneTypeError;
                reply();
                return;
            }

            string key = TimeHelper.ServerNow().ToString() + RandomHelper.RandInt64().ToString();
            scene.GetComponent<TokenComponent>().Remove(request.AccountId);
            scene.GetComponent<TokenComponent>().Add(request.AccountId, key);

            response.RelamKey = key;
            reply();
            await ETTask.CompletedTask;
        }
    }
}