namespace ET
{
    public static class DisConnectHelper
    {
        public static async ETTask DisConnect(this Session self)
        {
            if (self == null || self.IsDisposed)
            {
                return;
            }

            long instanceId = self.InstanceId;

            await TimerComponent.Instance.WaitAsync(1000);
                    
            if (instanceId != self.InstanceId)
            {
                return;
            }
            self.Dispose();
        }

        public static async ETTask KickPlayer(Player player,bool isException = false)
        {
            if (player == null || player.IsDisposed)
            {
                return;
            }

            long instanceId = player.InstanceId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate, player.Account.GetHashCode()))
            {
                if (player.IsDisposed || instanceId != player.InstanceId)
                {
                    return;
                }

                if (!isException)
                {
                    switch (player.PlayerState)
                    {
                        case PlayerState.DisConnect:
                            break;
                        case PlayerState.Gate:
                            break;
                        case PlayerState.Game:
                            //通知游戏逻辑服下线Unit角色逻辑，并存储数据到数据库
                            M2G_RequsetExitGame m2GRequsetExitGame =
                                    (M2G_RequsetExitGame)await MessageHelper.CallLocationActor(player.UnitId, new G2M_RequsetExitGame());
                            
                            //通知登录中心服移除账户登录信息
                            long logincenterConfigInstanceId = StartSceneConfigCategory.Instance.LoginCenterConfig.InstanceId;
                            var l2gremoveLoginRecord = (L2G_RemoveLoginRecord)await MessageHelper.CallActor(logincenterConfigInstanceId,
                                new G2L_RemoveLoginRecord() { ServerId = player.DomainZone(), AccountId = player.Account, });
                            
                            break;  
                    }
                }
                
                player.PlayerState = PlayerState.DisConnect;
                player.DomainScene().GetComponent<PlayerComponent>()?.Remove(player.Account);
                player?.Dispose();
            
                await TimerComponent.Instance.WaitAsync(300);
            }
        }
    }
}