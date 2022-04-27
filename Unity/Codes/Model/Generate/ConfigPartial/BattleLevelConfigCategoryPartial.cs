namespace ET
{
    public partial class BattleLevelConfigCategory
    {
        public BattleLevelConfig GetConfigByIndex(int index)
        {
            if (index < 0 || index >= this.list.Count)
            {
                Log.Error($"Get BattleLevelConifg Index Error: {index} ");
                return null;
            }
            return this.list[index];
        }
    }
}