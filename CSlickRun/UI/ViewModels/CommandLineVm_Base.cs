using CommunityToolkit.Mvvm.ComponentModel;
using CSlickRun.UI.ViewModels.Base;

namespace CSlickRun.UI.ViewModels;

/// <summary>
/// Basisklasse für das ViewModel der Befehlszeile.
/// </summary>
public partial class CommandLineVm_Base : ViewModelBase
{
    #region Private Fields

    [ObservableProperty] private bool alwaysOnTop;
    [ObservableProperty] private string? autoCompleteForegroundColor;
    [ObservableProperty] private bool autoStartup;
    [ObservableProperty] private string? caretColor;
    [ObservableProperty] private string? commandLineBackgroundColor;
    [ObservableProperty] private string? commandLineForegroundColor;
    [ObservableProperty] private string? commandLineHeight;
    [ObservableProperty] private string? commandLineWidth;
    [ObservableProperty] private string? autoCompleteBackgroundColor;
    [ObservableProperty] private string? borderInactiveColor;
    [ObservableProperty] private string? commandLineInactiveBackgroundColor;
    [ObservableProperty] private string? currentBackgroundColor;
    [ObservableProperty] private string? currentBorderColor;
    [ObservableProperty] private string? selectionColor;

    [ObservableProperty] private double left;
    [ObservableProperty] private double top;

    [ObservableProperty] private string? borderColor;

    #endregion
}