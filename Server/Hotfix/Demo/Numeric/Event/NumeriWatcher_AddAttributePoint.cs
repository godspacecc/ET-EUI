using ET.EventType;

namespace ET
{
    [NumericWatcher(NumericType.Spririt)]
    [NumericWatcher(NumericType.Power)]
    [NumericWatcher(NumericType.PhysicalStrength)]
    [NumericWatcher(NumericType.Agile)]
    public class NumeriWatcher_AddAttributePoint: INumericWatcher
    {
        public void Run(NumbericChange args)
        {
            if (!(args.Parent is Unit unit))
            {
                return;   
            }

            if (args.NumericType == NumericType.Power)
            {
                unit.GetComponent<NumericComponent>()[NumericType.DamageAdd] += 5;
            }
            
            if (args.NumericType == NumericType.PhysicalStrength)
            {
                unit.GetComponent<NumericComponent>()[NumericType.HpPct] += 1 * 10000;
            }
            
            if (args.NumericType == NumericType.Agile)
            {
                unit.GetComponent<NumericComponent>()[NumericType.ArmorFinalAdd] += 5;
            }
            
            if (args.NumericType == NumericType.Spririt)
            {
                unit.GetComponent<NumericComponent>()[NumericType.MPFinalAdd] += 1 * 10000;
            }
        }
    }
}