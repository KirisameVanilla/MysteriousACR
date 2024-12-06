using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using KirisameVanilla.Reference;

namespace KirisameVanilla.offGCD;

public class HighendOffGcd : ISlotResolver
{
    private static uint LastComboSpellId => Core.Resolve<MemApiSpell>().GetLastComboSpellId();

    public int Check()
    {
        var spell = GetSpell();
        if (spell == 0)
            return -1;
        if (DragonRotationEntry.Qt != null && !DragonRotationEntry.Qt.GetQt("能力技"))
            return -2;
        if (Core.Me.GetCurrTarget().DistanceToPlayer() > 10)
            return -3;
        if (DragonRotationEntry.Qt != null && !DragonRotationEntry.Qt.GetQt("爆发"))
            return -4;
        if (!Core.Resolve<MemApiSpell>().CheckActionChange(spell.GetSpell().Id).GetSpell().IsReadyWithCanCast())
            return -5;
        if (!spell.IsUnlock())
            return -6;
        return 1;
    }

    public void Build(Slot slot)
    {
        var spell = GetSpell().GetSpell();
        slot.Add(spell);
    }

    private static uint GetSpell()
    {
        if (DragonRotationEntry.Qt == null)
        {
            return 0;
        }

        if (IsReadyToCast(Skill.猛枪)) return Skill.猛枪;
        if (IsReadyToCast(Skill.战斗连祷)) return Skill.战斗连祷;
        if (IsReadyToCast(Skill.武神枪)) return Skill.武神枪;
        if (IsReadyToCast(Skill.跳跃)) return Skill.跳跃;
        if (IsReadyToCast(Skill.龙剑) && LastComboSpellId is Skill.龙尾大回旋 or Skill.龙爪龙牙 && !Skill.龙剑.RecentlyUsed(2000))
        {
            return Skill.龙剑;
        }

        if (IsReadyToCast(Skill.龙炎冲)) return Skill.龙炎冲;
        if (IsReadyToCast(Skill.死者之岸)) return Skill.死者之岸;
        if (IsReadyToCast(Skill.坠星冲)) return Skill.坠星冲;
        if (IsReadyWithAuraAndCast(Buff.渡星冲预备, Skill.渡星冲)) return Skill.渡星冲;
        if (IsReadyToCast(Skill.龙剑) && LastComboSpellId is Skill.苍穹刺 && !Skill.龙剑.RecentlyUsed(2000))
        {
            return Skill.龙剑;
        }

        if (IsReadyWithAuraAndCast(Buff.龙炎升预备, Skill.龙炎升)) return Skill.龙炎升;
        if (IsReadyToCast(Skill.死者之岸)) return Skill.死者之岸;
        if (IsReadyWithAuraAndCast(Buff.幻象冲预备, Skill.幻象冲)) return Skill.幻象冲;
        if (IsReadyToCast(Skill.死者之岸)) return Skill.死者之岸;
        if (Skill.天龙点睛.GetSpell().IsReadyWithCanCast()) return Skill.天龙点睛;

        return 0;

        bool IsReadyToCast(uint skill)
        {
            return skill.GetSpell().IsReadyWithCanCast() && DragonRotationEntry.Qt != null &&
                   DragonRotationEntry.Qt.GetQt(Skill.GetSkillName(skill));
        }

        bool IsReadyWithAuraAndCast(uint aura, uint skill, uint lastComboSkill = 0)
        {
            return Core.Me.HasAura(aura) && IsReadyToCast(skill) &&
                   (lastComboSkill == 0 || LastComboSpellId == lastComboSkill);
        }
    }
}