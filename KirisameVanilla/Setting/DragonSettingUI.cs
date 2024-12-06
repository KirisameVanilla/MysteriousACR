using AEAssist;
using AEAssist.MemoryApi;
using ImGuiNET;

namespace KirisameVanilla.Setting;

public class DragonSettingUI
{
    private static uint LastSpellId => Core.Resolve<MemApiSpell>().GetLastComboSpellId();

    public void Draw()
    {
        ImGui.Text("KirisameVanilla!");
        if (ImGui.Button("保存设置"))
        {
            if (DragonSetting.Instance != null) DragonSetting.Instance.Save();
        }
    }
}