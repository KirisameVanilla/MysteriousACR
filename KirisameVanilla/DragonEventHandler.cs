using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;

namespace KirisameVanilla;

public class DragonEventHandler : IRotationEventHandler
{
    public void OnResetBattle()
    {
        DragonBattleData.Instance = new DragonBattleData();
        DragonBattleData.Instance.Reset();
        DragonRotationEntry.Qt?.Reset();
    }

    public async Task OnNoTarget() => await Task.CompletedTask;

    public void OnSpellCastSuccess(Slot slot, Spell spell) { }

    public async Task OnPreCombat() => await Task.CompletedTask;

    public void AfterSpell(Slot slot, Spell spell) { }

    public void OnBattleUpdate(int currTime) { }

    public void OnEnterRotation()
    {
        // LogHelper.Print("我是？");
        Core.Resolve<MemApiChatMessage>().Toast2("要开始了哟~", 1, 2000);
    }

    public void OnExitRotation() { }

    public void OnTerritoryChanged() { }
}