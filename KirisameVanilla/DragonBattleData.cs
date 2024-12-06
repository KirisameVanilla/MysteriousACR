using Dalamud.Game.ClientState.Objects.Types;

namespace KirisameVanilla;

public class DragonBattleData
{
    public static DragonBattleData BattleData = new();

    // 当前战斗数据的引用实例，用于全局访问
    public static DragonBattleData Instance = BattleData;

    // 当前的战斗目标，可能为空
    public static IBattleChara? CurrTarget;

    // 重置战斗数据，创建一个新的 SumBattleData 实例
    public void Reset()
    {
        // 重置Instance为新的战斗数据对象
        Instance = new DragonBattleData();
    }
}