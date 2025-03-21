using System.IO;
using Newtonsoft.Json;
using CSlickRun.UI.ViewModels;

namespace CSlickRun.Logic;

/// <summary>
/// Class for managing application settings.
/// </summary>
public class Settings
{
    #region Properties

    /// <summary>
    /// Shortcut codes for the application.
    /// </summary>
    public List<string>? ShortCutCodes { get; set; }

    /// <summary>
    /// Background color of the command line.
    /// </summary>
    public string? CommandLineBackgroundColor { get; set; }

    /// <summary>
    /// Inactive background color of the command line.
    /// </summary>
    public string? CommandLineInactiveBackgroundColor { get; set; }

    /// <summary>
    /// Foreground color of the command line.
    /// </summary>
    public string? CommandLineForegroundColor { get; set; }

    /// <summary>
    /// Caret color of the command line.
    /// </summary>
    public string? CaretColor { get; set; }

    /// <summary>
    /// Foreground color of the autocomplete.
    /// </summary>
    public string? AutoCompleteForegroundColor { get; set; }

    /// <summary>
    /// Background color of the autocomplete.
    /// </summary>
    public string? AutoCompleteBackgroundColor { get; set; }

    /// <summary>
    /// Width of the command line.
    /// </summary>
    public string? CommandLineWidth { get; set; }

    /// <summary>
    /// Height of the command line.
    /// </summary>
    public string? CommandLineHeight { get; set; }

    /// <summary>
    /// Border color of the command line.
    /// </summary>
    public string? BorderColor { get; set; }

    /// <summary>
    /// Inactive border color of the command line.
    /// </summary>
    public string? BorderInactiveColor { get; set; }

    /// <summary>
    /// Selection color of the command line.
    /// </summary>
    public string? SelectionColor { get; set; }

    /// <summary>
    /// Indicates whether the application is always on top.
    /// </summary>
    public bool AlwaysOnTop { get; set; }

    /// <summary>
    /// Indicates whether the application starts automatically.
    /// </summary>
    public bool AutoStartup { get; set; }

    /// <summary>
    /// Indicates whether the history should be written.
    /// </summary>
    public bool WriteHistory { get; set; }

    #endregion Properties

    /// <summary>
    /// Saves the settings asynchronously.
    /// </summary>
    public async Task SaveAsync()
    {
        var objAsJson = JsonConvert.SerializeObject(this);
        await File.WriteAllTextAsync(Global.ConfigFile, objAsJson);
    }

    /// <summary>
    /// Gets the default settings as a JSON string.
    /// </summary>
    /// <returns>Default settings as JSON.</returns>
    public string GetDefaultSettingsAsJson()
    {
        ShortCutCodes = new List<string>
        {
            VirtualKeyCodes.ALTKEY,
            VirtualKeyCodes.SHIFTKEY,
            "q"
        };

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
    /// Loads the settings asynchronously.
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

