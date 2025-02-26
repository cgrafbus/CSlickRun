using CSlickRun.UI.ViewModels.Base;

namespace CSlickRun.UI.ViewModels;

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
    public string? BorderColor
    {
        get => _borderColor;
        set => SetField(ref _borderColor, value);
    }

    public string? AutoCompleteBackgroundColor
    {
        get => _autocompleteBackgroundColor;
        set => SetField(ref _autocompleteBackgroundColor, value);
    }

    public bool AutoStartup
    {
        get => _autoStartup;
        set => SetField(ref _autoStartup, value);
    }

    public bool AlwaysOnTop
    {
        get => _alwaysOnTop;
        set => SetField(ref _alwaysOnTop, value);
    }

    public bool StrgBool
    {
        get => _strgBool;
        set => SetField(ref _strgBool, value);
    }

    public string? CommandLineWidth
    {
        get => _commandLineWidth;
        set => SetField(ref _commandLineWidth, value);
    }

    public string? CommandLineHeight
    {
        get => _commandLineHeight;
        set => SetField(ref _commandLineHeight, value);
    }

    public bool AltBool
    {
        get => _altBool;
        set => SetField(ref _altBool, value);
    }

    public bool WinBool
    {
        get => _winBool;
        set => SetField(ref _winBool, value);
    }

    public bool ShiftBool
    {
        get => _shiftBool;
        set => SetField(ref _shiftBool, value);
    }

    public string? CommandLineBackgroundColor
    {
        get => _commandLineBackgroundColor;
        set => SetField(ref _commandLineBackgroundColor, value);
    }

    public string? CommandLineForegroundColor
    {
        get => _commandLineForegroundColor;
        set => SetField(ref _commandLineForegroundColor, value);
    }

    public string? CaretColor
    {
        get => _caretColor;
        set => SetField(ref _caretColor, value);
    }

    public string? AutoCompleteForegroundColor
    {
        get => _autoCompleteForegroundColor;
        set => SetField(ref _autoCompleteForegroundColor, value);
    }

    public AsyncRelayCommand SaveCommand { get; set; }

    public string? Shortcut
    {
        get => _shortcut;
        set => SetField(ref _shortcut, value);
    }
#endregion Properties
}