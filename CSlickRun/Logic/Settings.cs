using System.IO;
using Newtonsoft.Json;
using CSlickRun.UI.ViewModels;

namespace CSlickRun.Logic;

public class Settings
{
    #region Properties

    /// <summary>
    /// <see cref="SettingsVm_Base.Shortcut"/>
    /// </summary>
    public List<string>? ShortCutCodes { get; set; }

    /// <summary>
    /// <see cref="SettingsVm_Base.CommandLineBackgroundColor"/>
    /// </summary>
    public string? CommandLineBackgroundColor { get; set; }

    public string? CommandLineInactiveBackgroundColor { get; set; }

    /// <summary>
    /// <see cref="SettingsVm_Base.CommandLineForegroundColor"/>
    /// </summary>
    public string? CommandLineForegroundColor { get; set; }

    /// <summary>
    /// <see cref="SettingsVm_Base.CaretColor"/>
    /// </summary>
    public string? CaretColor { get; set; }

    /// <summary>
    /// <see cref="SettingsVm_Base.AutoCompleteForegroundColor"/>
    /// </summary>
    public string? AutoCompleteForegroundColor { get; set; }

    /// <summary>
    /// <see cref="SettingsVm_Base.AutoCompleteBackgroundColor"/>
    /// </summary>
    public string? AutoCompleteBackgroundColor { get; set; }

    /// <summary>
    /// <see cref="SettingsVm_Base.CommandLineWidth"/>
    /// </summary>
    public string? CommandLineWidth { get; set; }

    /// <summary>
    /// <see cref="SettingsVm_Base.CommandLineHeight"/>
    /// </summary>
    public string? CommandLineHeight { get; set; }

    /// <summary>
    /// <see cref="SettingsVm_Base.BorderColor"/>
    /// </summary>
    public string? BorderColor { get; set; }

    public string? BorderInactiveColor { get; set; }

    public string? SelectionColor { get; set; }

    /// <summary>
    /// <see cref="SettingsVm_Base.AlwaysOnTop"/>
    /// </summary>
    public bool AlwaysOnTop { get; set; }

    /// <summary>
    /// <see cref="SettingsVm_Base.AutoStartup"/>
    /// </summary>
    public bool AutoStartup { get; set; }


    #endregion Properties

    /// <summary>
    /// Speichert die Einstellungen
    /// </summary>
    public async Task SaveAsync()
    {
        var objAsJson = JsonConvert.SerializeObject(this);
        await File.WriteAllTextAsync(Global.ConfigFile, objAsJson);
    }

    /// <summary>
    /// Gibt die Default-Settings als JSON zurück
    /// </summary>
    /// <returns>Default Settings</returns>
    public string GetDefaultSettingsAsJson()
    {
        ShortCutCodes =
        [
            VirtualKeyCodes.ALTKEY,
            VirtualKeyCodes.SHIFTKEY,
            "q"
        ];

        CommandLineForegroundColor = "#ebebeb";
        CommandLineBackgroundColor = "#141414";
        CommandLineInactiveBackgroundColor = CommandLineBackgroundColor;
        CaretColor = "#b162d1";
        AutoCompleteForegroundColor = "#830bb3";
        AutoCompleteBackgroundColor = CommandLineBackgroundColor;
        BorderColor = AutoCompleteForegroundColor;
        BorderInactiveColor = BorderColor;
        SelectionColor = "#79B162D1";
        CommandLineWidth = "800";
        CommandLineHeight = "22";
        AlwaysOnTop = true;
        AutoStartup = false;
        return JsonConvert.SerializeObject(this);
    }

    /// <summary>
    /// Lädt die Einstellungen
    /// </summary>
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