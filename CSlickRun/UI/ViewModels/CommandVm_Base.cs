using System.Collections.ObjectModel;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

    [ObservableProperty]
    private ObservableCollection<Command> visibleCommands;

    [ObservableProperty]
    private ObservableCollection<Command> selectedCommands;

    [ObservableProperty]
    private string commandFilter;

    [ObservableProperty]
    private bool forbidShortcutExecution;

    [ObservableProperty]
    private ObservableCollection<CommandPath> commandPaths;

    [ObservableProperty]
    private Command currentCommand;

    [ObservableProperty]
    private int currentIndex;

    partial void OnCommandFilterChanging(string? oldValue, string? newValue)
    {
        FilterCommands(newValue);
    }

    private void FilterCommands(string? value)
    {
        VisibleCommands = new ObservableCollection<Command>(Commands
            .Where(x => x.Name.Contains(value ?? string.Empty, StringComparison.OrdinalIgnoreCase)).ToList());
        CurrentIndex = VisibleCommands.Count > 0 ? 0 : -1;
    }

    /// <summary>
    /// Flag, if Save is allowed
    /// </summary>
    public bool SaveAllowed => Commands.Any(c =>
        c.ItemStatus is ItemStatus.New or ItemStatus.Deleted or ItemStatus.Modified);
}