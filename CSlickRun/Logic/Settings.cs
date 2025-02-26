using System.IO;
using System.Net.Mime;
using System.Windows;
using System.Windows.Media;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CSlickRun.Logic;

public class Settings
{
    #region Properties
    public List<string>? ShortCutCodes { get; set; }

    public string? CommandLineBackgroundColor { get; set; }

    public string? CommandLineForegroundColor { get; set; }

    public string? CaretColor { get; set; }

    public string? AutoCompleteForegroundColor { get; set; }

    public string? AutoCompleteBackgroundColor { get; set; }

    public string? CommandLineWidth { get; set; }

    public string? CommandLineHeight { get; set; }
    public string? BorderColor { get; set; }

    public bool AlwaysOnTop { get; set; }
    public bool AutoStartup { get; set; }


    #endregion Properties

    public async Task SaveAsync()
    {
        var objAsJson = JsonConvert.SerializeObject(this);
        await File.WriteAllTextAsync(Global.ConfigFile, objAsJson);
    }

    public string GetDefaultSettingsAsJson()
    {
        ShortCutCodes =
        [
            VirtualKeyCodes.CTRLKEY,
            VirtualKeyCodes.ALTKEY,
            "q"
        ];

        CommandLineForegroundColor = "#ebebeb";
        CommandLineBackgroundColor = "#141414";
        CaretColor = "#b162d1";
        AutoCompleteForegroundColor = "#830bb3";
        AutoCompleteBackgroundColor = CommandLineBackgroundColor;
        BorderColor = AutoCompleteForegroundColor;
        CommandLineWidth = "800";
        CommandLineHeight = "22";
        AlwaysOnTop = true;
        AutoStartup = false;
        return JsonConvert.SerializeObject(this);
    }

    public async Task LoadAsync()
    {
        var jsonText = await File.ReadAllTextAsync(Global.ConfigFile);

        var settings = JsonConvert.DeserializeObject<Settings>(jsonText);

        if (settings == null)
        {
            return;
        }

        Mapper.MapClasses(settings, this);
    }
}