using System.Collections.ObjectModel;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CSlickRun.Logic;
using CSlickRun.UI.Controls;
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
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SaveAllowed))]
    private ObservableCollection<Command> commands;

    /// <summary>
    /// Momentanes ViewModel
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentSubView))]
    private UserControl? currentCommandView;

    public ISubView? CurrentSubView => CurrentCommandView?.DataContext as ISubView 
                             ?? throw new NotImplementedException
                                 ($"ViewModel of {CurrentCommandView?.Name} must implement the ISubView Interface");

    /// <summary>
    /// Ist Speichern erlaubt?
    /// </summary>
    public bool SaveAllowed => Commands.Any(c =>
        c.ItemStatus is ItemStatus.New or ItemStatus.Deleted or ItemStatus.Modified);
}