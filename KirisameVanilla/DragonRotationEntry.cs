using System.Runtime.Versioning;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;
using KirisameVanilla.GCD;
using KirisameVanilla.offGCD;
using KirisameVanilla.QT;
using KirisameVanilla.Setting;

[assembly: SupportedOSPlatform("windows")]

namespace KirisameVanilla;

public class DragonRotationEntry : IRotationEntry
{
    public string AuthorName { get; set; } = "KirisameVanilla";
    private static string OverlayTitle => "MainQT";

    public Rotation Build(string settingFolder)
    {
        DragonSetting.Build(settingFolder);

        BuildQt();

        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.Dragoon,
            AcrType = AcrType.Normal,
            MinLevel = 90,
            MaxLevel = 100,
            Description = "100级测试龙骑"
        };

        rot.SetRotationEventHandler(new DragonEventHandler());

        return rot;
    }

    public readonly List<SlotResolverData> SlotResolvers =
    [
        new(new HighendOffGcd(), SlotMode.OffGcd),
        new(new HighendGcd(), SlotMode.Gcd)
    ];

    public static JobViewWindow? Qt { get; private set; }

    public IRotationUI GetRotationUI()
    {
        if (Qt is not null) return Qt;
        throw new InvalidOperationException();
    }

    private readonly DragonSettingUI _settingUi = new();

    public void OnDrawSetting() => _settingUi.Draw();

    public void BuildQt()
    {
        if (DragonSetting.Instance == null) return;
        Qt = new DragonQT(DragonSetting.Instance.JobViewSave, DragonSetting.Instance.Save, OverlayTitle);
    }

    public void Dispose() { }
}