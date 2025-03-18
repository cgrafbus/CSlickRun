using System.Collections.ObjectModel;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CSlickRun.Logic;
using CSlickRun.UI.ViewModels.Base;

namespace CSlickRun.UI.ViewModels;

/// <summary>
/// Base-Klasse zum überliegenden Command-ViewModel
/// </summary>
public partial class CommandVm_Base : ViewModelBase
{
    /// <summary>
    /// Alle Commands
    /// </summary>
    [ObservableProperty] private ObservableCollection<Command> commands;

    /// <summary>
    /// Momentanes ViewModel
    /// </summary>
    [ObservableProperty] private UserControl? currentCommandView;
}