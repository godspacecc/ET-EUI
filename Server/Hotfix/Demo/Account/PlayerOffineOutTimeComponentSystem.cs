using System;

namespace ET
{
    [Timer(TimerType.PlayerOffineTimeout)]
    public class PlayerOffineOutTime: ATimer<PlayerOffineOutTimeComponent>
    {
        public override void Run(PlayerOffineOutTimeComponent self)
        {
            try
            {
                self.KickPlayer();
            }
            catch (Exception e)
            {
                Log.Error($"move timer error: {self.Id}\n{e}");
            }
        }
    }
    
    [ObjectSystem]
    public class PlayerOffineOutTimeComponentAwakeSystem: AwakeSystem<PlayerOffineOutTimeComponent>
    {
        public override void Awake(PlayerOffineOutTimeComponent self)
        {
            self.Timer = TimerComponent.Instance.NewOnceTimer(TimeHelper.ServerNow() + 10000, TimerType.PlayerOffineTimeout, self);
        }
    }
    
    public class PlayerOffineOutTimeComponentDestroySystem:DestroySystem<PlayerOffineOutTimeComponent>
    {
        public override void Destroy(PlayerOffineOutTimeComponent self)
        {
            TimerComponent.Instance.Remove(ref self.Timer);
        }
    }
    
    public static class GateUnitDeleteComponentSystem
    {
        public static void KickPlayer(this PlayerOffineOutTimeComponent self)
        {
            DisConnectHelper.KickPlayer(self.GetParent<Player>()).Coroutine();
        }
    }
}