using CSlickRun.UI.ViewModels.Base;

namespace CSlickRun.UI.ViewModels;

/// <summary>
/// Basisklasse für das SettingsViewModel
/// </summary>
public class SettingsVm_Base : ViewModelBase
{
    #region Private Fields

    private bool _altBool;
    private bool _alwaysOnTop;
    private string? _autocompleteBackgroundColor;
    private string? _autoCompleteForegroundColor;
    private bool _autoStartup;
    private string? _borderColor;
    private string? _caretColor;
    private string? _commandLineBackgroundColor;
    private string? _commandLineForegroundColor;
    private string? _commandLineHeight;
    private string? _commandLineWidth;
    private bool _shiftBool;
    private string? _shortcut;
    private bool _strgBool;
    private bool _winBool;

    #endregion

    #region Properties

    /// <summary>
    /// Farbe der Border vom CommandLineWindow
    /// </summary>
    public string? BorderColor
    {
        get => _borderColor;
        set => SetField(ref _borderColor, value);
    }

    /// <summary>
    /// Hintergrundfarbe des Autocompletevorschlags vom CommandLineWindow
    /// </summary>
    public string? AutoCompleteBackgroundColor
    {
        get => _autocompleteBackgroundColor;
        set => SetField(ref _autocompleteBackgroundColor, value);
    }

    /// <summary>
    /// Soll die Applikation beim Start des Computers geöffnet werden?
    /// </summary>
    public bool AutoStartup
    {
        get => _autoStartup;
        set => SetField(ref _autoStartup, value);
    }

    /// <summary>
    /// Soll die CommandLine über allem anderen stehen?
    /// </summary>
    public bool AlwaysOnTop
    {
        get => _alwaysOnTop;
        set => SetField(ref _alwaysOnTop, value);
    }

    /// <summary>
    /// Ist der Strg-Knopf Teil des Hotkeys?
    /// </summary>
    public bool StrgBool
    {
        get => _strgBool;
        set => SetField(ref _strgBool, value);
    }

    /// <summary>
    /// Breite der Commandline
    /// </summary>
    public string? CommandLineWidth
    {
        get => _commandLineWidth;
        set => SetField(ref _commandLineWidth, value);
    }

    /// <summary>
    /// Höhe der Commandline
    /// </summary>
    public string? CommandLineHeight
    {
        get => _commandLineHeight;
        set => SetField(ref _commandLineHeight, value);
    }

    /// <summary>
    /// Ist der Alt-Knopf Teil des Hotkeys?
    /// </summary>
    public bool AltBool
    {
        get => _altBool;
        set => SetField(ref _altBool, value);
    }

    /// <summary>
    /// Ist der Windows-Knopf Teil des Hotkeys?
    /// </summary>
    public bool WinBool
    {
        get => _winBool;
        set => SetField(ref _winBool, value);
    }

    /// <summary>
    /// Ist der Shift-Knopf Teil des Hotkeys?
    /// </summary>
    public bool ShiftBool
    {
        get => _shiftBool;
        set => SetField(ref _shiftBool, value);
    }

    /// <summary>
    /// Hintergrundfarbe der Commandline
    /// </summary>
    public string? CommandLineBackgroundColor
    {
        get => _commandLineBackgroundColor;
        set => SetField(ref _commandLineBackgroundColor, value);
    }

    /// <summary>
    /// Vordergrundfarbe der Commandline
    /// </summary>
    public string? CommandLineForegroundColor
    {
        get => _commandLineForegroundColor;
        set => SetField(ref _commandLineForegroundColor, value);
    }

    /// <summary>
    /// Farbe des Carets der CommandLine
    /// </summary>
    public string? CaretColor
    {
        get => _caretColor;
        set => SetField(ref _caretColor, value);
    }

    /// <summary>
    /// Vordergrundfarbe des Autocompletes der CommandLine
    /// </summary>
    public string? AutoCompleteForegroundColor
    {
        get => _autoCompleteForegroundColor;
        set => SetField(ref _autoCompleteForegroundColor, value);
    }


    /// <summary>
    /// Speicher-Command
    /// </summary>
    public AsyncRelayCommand SaveCommand { get; set; }

    /// <summary>
    /// Shortcut-Key
    /// </summary>
    public string? Shortcut
    {
        get => _shortcut;
        set => SetField(ref _shortcut, value);
    }
#endregion Properties
}