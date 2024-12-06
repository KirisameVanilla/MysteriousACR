using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using KirisameVanilla.Reference;

namespace KirisameVanilla.GCD;

public class HighendGcd : ISlotResolver
{
    private static uint LastComboSpellId => Core.Resolve<MemApiSpell>().GetLastComboSpellId();

    public int Check()
    {
        if (GetSpell() == 0)
            return -1;
        if (Core.Resolve<MemApiSpell>().GetActionInRangeOrLoS(GetSpell()) == 566)
            return -2;
        if (Core.Me.Level < 90)
            return -3;
        if (DragonRotationEntry.Qt != null && !DragonRotationEntry.Qt.GetQt("基础GCD"))
            return -4;
        return 1;
    }

    public void Build(Slot slot)
    {
        var spell = GetSpell().GetSpell();
        slot.Add(Core.Resolve<MemApiSpell>().CheckActionChange(spell.Id).GetSpell());
    }

    private uint GetSpell()
    {
        var dot = Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(Buff.樱花缭乱, 7000);

        switch (LastComboSpellId)
        {
            case Skill.樱花缭乱:
                return Skill.龙尾大回旋;
            case Skill.苍穹刺:
                return Skill.龙爪龙牙;
            case Skill.龙尾大回旋 or Skill.龙爪龙牙:
                return Skill.云蒸龙变;
        }

        if (DragonRotationEntry.Qt != null && DragonRotationEntry.Qt.GetQt("AOE") &&
            TargetHelper.GetEnemyCountInsideRect(Core.Me, Core.Me.GetCurrTarget(), 10, 4) >= 3)
        {
            return AOE();
        }

        return dot ? FullThrust() : Sakura();
    }

    private static uint Sakura()
    {
        return LastComboSpellId switch
        {
            Skill.龙眼雷电 or Skill.精准刺 => Core.Me.Level >= 96 ? Skill.螺旋击 : Skill.开膛枪,
            Skill.贯通刺 or Skill.前冲刺 => Skill.苍穹刺,
            Skill.开膛枪 or Skill.螺旋击 => Skill.樱花缭乱,
            _ => Core.Me.HasAura(Buff.龙眼) ? Skill.龙眼雷电 : Skill.精准刺
        };
    }

    private static uint FullThrust()
    {
        return LastComboSpellId switch
        {
            Skill.龙眼雷电 or Skill.精准刺 => Core.Me.Level >= 96 ? Skill.贯通刺 : Skill.前冲刺,
            Skill.开膛枪 or Skill.螺旋击 => Skill.樱花缭乱,
            Skill.贯通刺 or Skill.前冲刺 => Skill.苍穹刺,
            _ => Core.Me.HasAura(Buff.龙眼) ? Skill.龙眼雷电 : Skill.精准刺
        };
    }

    private static uint AOE()
    {
        return LastComboSpellId switch
        {
            Skill.死天枪 or Skill.龙眼苍穹 when Core.Me.Level >= 62 => Skill.音速刺,
            Skill.音速刺 when Core.Me.Level >= 72 => Skill.山境酷刑,
            Skill.山境酷刑 when Core.Me.Level >= 82 => Skill.龙眼苍穹,
            _ => Skill.死天枪
        };
    }
}