using System.Numerics;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;

namespace KirisameVanilla.Setting;

public class DragonSetting
{
    public static DragonSetting? Instance;

    public JobViewSave JobViewSave = new()
    {
        MainColor = new Vector4(0 / 255f, 62 / 255f, 124 / 255f, 0.8f),
        QtHotkeySize = new Vector2(80f, 80f),
        QtButtonSize = new Vector2(100f, 50f),
        QtWindowBgAlpha = 0.1f,
        AutoReset = false,
        ShowQT = true,
        ShowHotkey = true
    };

    #region 标准模板代码 可以直接复制后改掉类名即可

    // 保存设定的文件路径
    private static string? _path;

    // 构建并加载设置文件
    public static void Build(string settingPath)
    {
        // 设置文件路径，文件名为类名+".json"
        _path = Path.Combine(settingPath, nameof(DragonSetting), "KirisameVanilla.json");

        // 如果文件不存在，创建默认设置并保存
        if (!File.Exists(_path))
        {
            Instance = new DragonSetting();
            Instance.Save();
            return;
        }

        // 尝试从文件中读取设定
        try
        {
            Instance = JsonHelper.FromJson<DragonSetting>(File.ReadAllText(_path));
        }
        catch (Exception e)
        {
            // 读取失败时，创建默认设置并记录错误日志
            Instance = new DragonSetting();
            LogHelper.Error(e.ToString());
        }
    }

    // 保存当前设置到文件
    public void Save()
    {
        // 创建目录并保存文件为JSON格式
        Directory.CreateDirectory(Path.GetDirectoryName(_path));
        File.WriteAllText(_path, JsonHelper.ToJson(this));
    }

    #endregion
}