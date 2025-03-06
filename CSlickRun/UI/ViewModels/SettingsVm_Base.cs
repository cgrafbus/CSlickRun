using CommunityToolkit.Mvvm.ComponentModel;
using CSlickRun.UI.ViewModels.Base;

namespace CSlickRun.UI.ViewModels;

/// <summary>
/// Basisklasse für das SettingsViewModel
/// </summary>
public partial class SettingsVm_Base : ViewModelBase
{
    #region Private Fields

    [ObservableProperty] private bool altBool;
    [ObservableProperty] private bool alwaysOnTop;
    [ObservableProperty] private string? autocompleteBackgroundColor;
    [ObservableProperty] private string? autoCompleteForegroundColor;
    [ObservableProperty] private bool autoStartup;
    [ObservableProperty] private string? borderColor;
    [ObservableProperty] private string? borderInactiveColor;
    [ObservableProperty] private string? selectionColor;
    [ObservableProperty] private string? caretColor;
    [ObservableProperty] private string? commandLineBackgroundColor;
    [ObservableProperty] private string? commandLineInactiveBackgroundColor;
    [ObservableProperty] private string? commandLineForegroundColor;
    [ObservableProperty] private string? commandLineHeight;
    [ObservableProperty] private string? commandLineWidth;
    [ObservableProperty] private bool shiftBool;
    [ObservableProperty] private string? shortcut;
    [ObservableProperty] private bool strgBool;
    [ObservableProperty] private bool winBool;

    #endregion
}