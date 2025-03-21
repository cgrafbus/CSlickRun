using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CSlickRun.Logic;
using CSlickRun.UI.Controls;
using CSlickRun.UI.ViewModels.Base;

namespace CSlickRun.UI.ViewModels;

public partial class CommandListVm : ViewModelBase
{
    /// <summary>
    /// Current index of selected Command
    /// </summary>
    [ObservableProperty] private int? _currentIndex;

    /// <summary>
    /// Command filter
    /// </summary>
    [ObservableProperty] private string? commandFilter;

    /// <summary>
    /// 
    /// </summary>
    [ObservableProperty] private bool forbidShortcutExecution;


    /// <summary>
    /// Parent-ViewModel
    /// </summary>
    [ObservableProperty] private CommandVm parentVm;

    /// <summary>
    /// Currently selected Command
    /// </summary>
    [ObservableProperty] private Command? selectedCommand;

    /// <summary>
    /// List of visible Commands
    /// </summary>
    [ObservableProperty] private ObservableCollection<Command> visibleCommands;


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="parentvm">Parent-ViewModel</param>
    public CommandListVm(CommandVm parentvm)
    {
        parentVm = parentvm;
        VisibleCommands = Commands;
        CurrentIndex = 0;
        selectedCommand = Commands.FirstOrDefault();
    }

    /// <inheritdoc cref="CommandVm_Base.Commands" />
    public ObservableCollection<Command> Commands => ParentVm.Commands ?? [];

    partial void OnCommandFilterChanging(string? oldValue, string? newValue)
    {
        FilterCommands(newValue);
    }

    /// <summary>
    /// Filters the commands on the ui by the name
    /// </summary>
    /// <param name="value">Filter</param>
    private void FilterCommands(string? value)
    {
        VisibleCommands = new ObservableCollection<Command>(Commands
            .Where(x => x.Name.Contains(value ?? string.Empty, StringComparison.OrdinalIgnoreCase)).ToList());
        CurrentIndex = VisibleCommands.Count > 0 ? 0 : -1;
    }

    /// <summary>
    /// Decreases index by one
    /// </summary>
    [RelayCommand]
    private void MoveIndexUp()
    {
        if (CurrentIndex > 0)
        {
            CurrentIndex -= 1;
        }
    }

    /// <summary>
    /// Increases index by one
    /// </summary>
    [RelayCommand]
    private void MoveIndexDown()
    {
        if (CurrentIndex < Commands.Count)
        {
            CurrentIndex += 1;
        }
    }

    /// <summary>
    /// Activates search mode
    /// </summary>
    [RelayCommand]
    private void ActivateSearch()
    {
        ForbidShortcutExecution = true;
    }

    /// <inheritdoc cref="CommandVm.Edit"/>
    [RelayCommand]
    private void ExecuteEdit()
    {
        if (SelectedCommand != null)
        {
            ParentVm.EditCommand.Execute(SelectedCommand);
        }
    }

    /// <inheritdoc cref="CommandVm.Delete"/>
    [RelayCommand]
    private void ExecuteDelete()
    {
        if (SelectedCommand != null)
        {
            ParentVm.DeleteCommand.Execute(SelectedCommand);
        }
    }
}