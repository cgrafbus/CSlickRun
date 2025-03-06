using CSlickRun.UI.ViewModels.Base;

namespace CSlickRun.UI.ViewModels;

/// <summary>
/// Basisklasse für das ViewModel der Befehlszeile.
/// </summary>
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
    private string? _borderInactiveColor;
    private string? _commandLineInactiveBackgroundColor;
    private string? _currentBackgroundColor;
    private string? _currentBorderColor;
    private string? _selectionColor;
    private double _left;
    private double _top;
    #endregion

    #region Properties
    /// <summary>
    /// Die Farbe des Rahmens.
    /// </summary>
    public string? BorderColor
    {
        get => _borderColor;
        set => SetField(ref _borderColor, value);
    }

    /// <summary>
    /// Farbe der Border vom CommandLineWindow wenn inaktiv
    /// </summary>
    public string? BorderInactiveColor
    {
        get => _borderInactiveColor;
        set => SetField(ref _borderInactiveColor, value);
    }

    /// <summary>
    /// Hintergrundfarbe der CommandLine wenn inaktiv
    /// </summary>
    public string? CommandLineInactiveBackgroundColor
    {
        get => _commandLineInactiveBackgroundColor;
        set => SetField(ref _commandLineInactiveBackgroundColor, value);
    }

    /// <summary>
    /// Farbe der Textselektion vom CommandLineWindow
    /// </summary>
    public string? SelectionColor
    {
        get => _selectionColor;
        set => SetField(ref _selectionColor, value);
    }

    /// <summary>
    /// Die Hintergrundfarbe der automatischen Vervollständigung.
    /// </summary>
    public string? AutoCompleteBackgroundColor
    {
        get => _autoCompleteBackgroundColor;
        set => SetField(ref _autoCompleteBackgroundColor, value);
    }

    /// <summary>
    /// Gibt an, ob die Anwendung automatisch gestartet wird.
    /// </summary>
    public bool AutoStartup
    {
        get => _autoStartup;
        set => SetField(ref _autoStartup, value);
    }

    /// <summary>
    /// Gibt an, ob das Fenster immer im Vordergrund bleibt.
    /// </summary>
    public bool AlwaysOnTop
    {
        get => _alwaysOnTop;
        set => SetField(ref _alwaysOnTop, value);
    }

    /// <summary>
    /// Die Breite der Befehlszeile.
    /// </summary>
    public string? CommandLineWidth
    {
        get => _commandLineWidth;
        set => SetField(ref _commandLineWidth, value);
    }

    /// <summary>
    /// Die Höhe der Befehlszeile.
    /// </summary>
    public string? CommandLineHeight
    {
        get => _commandLineHeight;
        set => SetField(ref _commandLineHeight, value);
    }

    /// <summary>
    /// Die Hintergrundfarbe der Befehlszeile.
    /// </summary>
    public string? CommandLineBackgroundColor
    {
        get => _commandLineBackgroundColor;
        set => SetField(ref _commandLineBackgroundColor, value);
    }

    /// <summary>
    /// Die Vordergrundfarbe der Befehlszeile.
    /// </summary>
    public string? CommandLineForegroundColor
    {
        get => _commandLineForegroundColor;
        set => SetField(ref _commandLineForegroundColor, value);
    }

    /// <summary>
    /// Die Farbe des Cursors.
    /// </summary>
    public string? CaretColor
    {
        get => _caretColor;
        set => SetField(ref _caretColor, value);
    }

    /// <summary>
    /// Die Vordergrundfarbe der automatischen Vervollständigung.
    /// </summary>
    public string? AutoCompleteForegroundColor
    {
        get => _autoCompleteForegroundColor;
        set => SetField(ref _autoCompleteForegroundColor, value);
    }

    public string? CurrentBackgroundColor
    {
        get => _currentBackgroundColor;
        set => SetField(ref _currentBackgroundColor, value);
    }

    public string? CurrentBorderColor
    {
        get => _currentBorderColor;
        set => SetField(ref _currentBorderColor, value);
    }

    /// <summary>
    /// Die linke Position des Fensters.
    /// </summary>
    public double Left
    {
        get => _left;
        set => SetField(ref _left, value);
    }

    /// <summary>
    /// Die obere Position des Fensters.
    /// </summary>
    public double Top
    {
        get => _top;
        set => SetField(ref _top, value);
    }


    #endregion
}
