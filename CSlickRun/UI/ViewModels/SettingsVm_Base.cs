using CommunityToolkit.Mvvm.ComponentModel;
using CSlickRun.UI.ViewModels.Base;

namespace CSlickRun.UI.ViewModels;

/// <summary>
/// Base class for the Settings ViewModel.
/// </summary>
public partial class SettingsVm_Base : ViewModelBase
{
    #region Private Fields

    /// <summary>
    /// Indicates whether the Alt key is part of the shortcut.
    /// </summary>
    [ObservableProperty] private bool altBool;

    /// <summary>
    /// Indicates whether the application is always on top.
    /// </summary>
    [ObservableProperty] private bool alwaysOnTop;

    /// <summary>
    /// Background color of the autocomplete.
    /// </summary>
    [ObservableProperty] private string? autoCompleteBackgroundColor;

    /// <summary>
    /// Foreground color of the autocomplete.
    /// </summary>
    [ObservableProperty] private string? autoCompleteForegroundColor;

    /// <summary>
    /// Indicates whether the application starts automatically.
    /// </summary>
    [ObservableProperty] private bool autoStartup;

    /// <summary>
    /// Border color of the command line.
    /// </summary>
    [ObservableProperty] private string? borderColor;

    /// <summary>
    /// Inactive border color of the command line.
    /// </summary>
    [ObservableProperty] private string? borderInactiveColor;

    /// <summary>
    /// Selection color of the command line.
    /// </summary>
    [ObservableProperty] private string? selectionColor;

    /// <summary>
    /// Caret color of the command line.
    /// </summary>
    [ObservableProperty] private string? caretColor;

    /// <summary>
    /// Background color of the command line.
    /// </summary>
    [ObservableProperty] private string? commandLineBackgroundColor;

    /// <summary>
    /// Inactive background color of the command line.
    /// </summary>
    [ObservableProperty] private string? commandLineInactiveBackgroundColor;

    /// <summary>
    /// Foreground color of the command line.
    /// </summary>
    [ObservableProperty] private string? commandLineForegroundColor;

    /// <summary>
    /// Height of the command line.
    /// </summary>
    [ObservableProperty] private string? commandLineHeight;

    /// <summary>
    /// Width of the command line.
    /// </summary>
    [ObservableProperty] private string? commandLineWidth;

    /// <summary>
    /// Indicates whether the Shift key is part of the shortcut.
    /// </summary>
    [ObservableProperty] private bool shiftBool;

    /// <summary>
    /// The shortcut key.
    /// </summary>
    [ObservableProperty] private string? shortcut;

    /// <summary>
    /// Indicates whether the Ctrl key is part of the shortcut.
    /// </summary>
    [ObservableProperty] private bool strgBool;

    /// <summary>
    /// Indicates whether the Win key is part of the shortcut.
    /// </summary>
    [ObservableProperty] private bool winBool;

    /// <summary>
    /// Indicates whether the history should be written.
    /// </summary>
    [ObservableProperty] private bool writeHistory;

    [ObservableProperty] private bool useKeyPressBehaviour;

    #endregion
}



