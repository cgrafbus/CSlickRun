using CSlickRun.UI.ViewModels.Base;

namespace CSlickRun.UI.ViewModels;

public class CommandLineVm_Base : ViewModelBase
{
    #region Private Fields
    private bool _alwaysOnTop;
    private string? _autoCompleteForegroundColor;
    private bool _autoStartup;
    private string? _caretColor;
    private string? _commandLineBackgroundColor;
    private string? _commandLineForegroundColor;
    private string? _commandLineHeight;
    private string? _commandLineWidth;
    private string? _autoCompleteBackgroundColor;
    private string? _borderColor;
    private double _left;
    private double _top;

    #endregion

    #region Properties
    public string? BorderColor
    {
        get => _borderColor;
        set => SetField(ref _borderColor, value);
    }

    public string? AutoCompleteBackgroundColor
    {
        get => _autoCompleteBackgroundColor;
        set => SetField(ref _autoCompleteBackgroundColor, value);
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

    public double Left
    {
        get => _left;
        set => SetField(ref _left, value);
    }

    public double Top
    {
        get => _top;
        set => SetField(ref _top, value);
    }
#endregion
}