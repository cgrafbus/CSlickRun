using System.Collections.ObjectModel;
using System.Windows.Input;
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

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AnyCommandsSelected))]
    [NotifyPropertyChangedFor(nameof(SelectedCommand))]
    private List<Command?>? selectedCommands;

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
        SelectedCommands = new List<Command?>([Commands.FirstOrDefault() ?? null]);
    }

    public bool AnyCommandsSelected => SelectedCommands?.Any() == true;

    public Command SelectedCommand => SelectedCommands?.FirstOrDefault() ?? new Command();

    /// <inheritdoc cref="CommandVm_Base.Commands" />
    public ObservableCollection<Command> Commands => ParentVm.Commands ?? [];

    partial void OnSelectedCommandsChanged(List<Command?>? value)
    {
        Console.Write("a");
    }

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
    private void ExecuteEdit(object? obj)
    {
        if (obj is Command comm)
        {
            ParentVm.EditCommand.Execute(comm);
            return;
        }
        ParentVm.EditCommand.Execute(SelectedCommand);
    }

    /// <inheritdoc cref="CommandVm.Delete"/>
    [RelayCommand]
    private void ExecuteDelete(object? obj)
    {
        if (obj is Command comm && obj is not KeyPressBehavior && !SelectedCommands?.Contains(comm) == true)
        {
            ParentVm.DeleteCommand.Execute(comm);
            return;
        }
        ParentVm.DeleteCommand.Execute(SelectedCommands);
    }

    /// <inheritdoc cref="CommandVm.Save"/>
    [RelayCommand]
    private void Save()
    {
        ParentVm.SaveCommand.Execute(null);
    }

    /// <inheritdoc cref="CommandVm.Add"/>
    [RelayCommand]
    private void Add()
    {
        ParentVm.AddCommand.Execute(new Command());
    }

    [RelayCommand]
    private async Task Export()
    {
        if (SelectedCommands?.Any() == true)
        {
            await CommandFactory.ExportCommands(SelectedCommands.ToList());
        }
    }

    [RelayCommand]
    private async Task Import()
    {
        var importedCommands = await CommandFactory.ImportCommands();
        if (importedCommands != null)
        {
            foreach (var command in importedCommands.OfType<Command>())
            {
                command.ItemStatus = ItemStatus.New;
                Commands.Add(command);
            }
        }
    }
}