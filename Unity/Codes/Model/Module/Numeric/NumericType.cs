namespace ET
{
	// 这个可弄个配置表生成
    public static class NumericType
    {
	    public const int Max = 10000;
	    
	    public const int Speed = 1000;
	    public const int SpeedBase = Speed * 10 + 1;
	    public const int SpeedAdd = Speed * 10 + 2;
	    public const int SpeedPct = Speed * 10 + 3;
	    public const int SpeedFinalAdd = Speed * 10 + 4;
	    public const int SpeedFinalPct = Speed * 10 + 5;

	    public const int Damage = 1011;
	    public const int DamageBase = Damage * 10 + 1;
	    public const int DamageAdd = Damage * 10 + 2;
	    public const int DamagePct = Damage * 10 + 3;
	    public const int DamageFinalAdd = Damage * 10 + 4;
	    public const int DamageFinalPct = Damage * 10 + 5;
	    
	    public const int AdditionDamage = 1012; //伤害追加

	    public const int Hp = 1013;
	    public const int HpBase = Hp * 10 + 1;
	    public const int HpAdd = Hp * 10 + 2;
	    public const int HpPct = Hp * 10 + 3;
	    public const int HpFinalAdd = Hp * 10 + 4;
	    public const int HpFinalPct = Hp * 10 + 5;
	    
	    public const int MP = 1014;
	    public const int MPBase = MP * 10 + 1;
	    public const int MPAdd = MP * 10 + 2;
	    public const int MPPct = MP * 10 + 3;
	    public const int MPFinalAdd = MP * 10 + 4;
	    public const int MPFinalPct = MP * 10 + 5;
	    
	    public const int Armor = 1015; //护甲
	    public const int ArmorBase = Armor * 10 + 1;
	    public const int ArmorAdd = Armor * 10 + 2;
	    public const int ArmorPct = Armor * 10 + 3;
	    public const int ArmorFinalAdd = Armor * 10 + 4;
	    public const int ArmorFinalPct = Armor * 10 + 5;
	    
	    public const int AdditionArmor = 1016; //护甲追加

	    public const int Power = 3001; //力量
	    public const int PhysicalStrength = 3002; //体力
	    public const int Agile = 3003; //敏捷
	    public const int Spririt = 3004; //精神
	    public const int AttributePoint = 3005;//属性点
	    public const int CombatEffectiveness = 3006; //战力
	    public const int Level = 3007;
	    public const int Gold = 3008;
	    public const int Exp = 3009;
	    public const int AdventureState = 3010;//关卡冒险状态
	    public const int DyingState = 3011;//垂死状态
	    public const int AdventureStartTime = 3012;//关卡开始冒险时间
    }
}
