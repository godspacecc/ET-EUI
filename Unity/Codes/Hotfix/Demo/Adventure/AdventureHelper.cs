namespace ET
{
    public static class AdventureHelper
    {
        public static async ETTask<int> RequesStartGameLevel(Scene zoneScene, int levelId)
        {
            //M2C_StartSceneChange
            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }
    }
}