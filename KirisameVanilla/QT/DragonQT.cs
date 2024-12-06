using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;

namespace KirisameVanilla.QT;

public class DragonQT : JobViewWindow
{
    public DragonQT(JobViewSave jobViewSave, Action save, string name) : base(jobViewSave, save, name)
    {
        AddQt("爆发", true);
        AddQt("AOE", true);
        AddQt("基础GCD", true);
        AddQt("能力技", true);
        AddQt("猛枪", true);
        AddQt("战斗连祷", true);
        AddQt("龙剑", true);
        AddQt("武神枪", true);
        AddQt("高跳", true);
        AddQt("龙炎冲", true);
        AddQt("龙炎升", true);
        AddQt("死者之岸", true);
        AddQt("坠星冲", true);
        AddQt("渡星冲", true);
        AddQt("幻象冲", true);
        AddHotkey("LB", new HotKeyResolver_LB());
    }
}