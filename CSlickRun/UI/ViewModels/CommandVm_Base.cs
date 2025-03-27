using System.Collections.ObjectModel;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CSlickRun.Logic;
using CSlickRun.UI.Controls;
using CSlickRun.UI.ViewModels.Base;

namespace CSlickRun.UI.ViewModels;

/// <summary>
/// Base Class for Command-ViewModel
/// </summary>
public partial class CommandVm_Base : ViewModelBase
{
    /// <summary>
    /// All Commands
    /// </summary>
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SaveAllowed))]
    private ObservableCollection<Command> commands;

    /// <summary>
    /// Current ViewModel
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentViewIsList))]
    private UserControl? currentCommandView;

    public bool CurrentViewIsList => CurrentCommandView?.DataContext is CommandListVm;

    /// <summary>
    /// Flag, if Save is allowed
    /// </summary>
    public bool SaveAllowed => Commands.Any(c =>
        c.ItemStatus is ItemStatus.New or ItemStatus.Deleted or ItemStatus.Modified);
}