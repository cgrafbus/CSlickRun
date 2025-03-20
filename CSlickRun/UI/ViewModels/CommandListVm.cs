using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CSlickRun.Logic;
using CSlickRun.UI.Controls;
using CSlickRun.UI.ViewModels.Base;

namespace CSlickRun.UI.ViewModels;

public partial class CommandListVm : ViewModelBase, ISubView
{
    [ObservableProperty] private int? _currentIndex;

    [ObservableProperty] private string? commandFilter;

    [ObservableProperty] private bool forbidShortcutExecution;


    /// <summary>
    ///     Parent-ViewModel
    /// </summary>
    [ObservableProperty] private CommandVm parentVm;

    /// <summary>
    ///     Momentan ausgewählter Befehl
    /// </summary>
    [ObservableProperty] private Command? selectedCommand;

    [ObservableProperty] private ObservableCollection<Command> visibleCommands;


    /// <summary>
    ///     Konstruktor
    /// </summary>
    /// <param name="parentvm">Parent-ViewModel</param>
    public CommandListVm(CommandVm parentvm)
    {
        parentVm = parentvm;
        VisibleCommands = Commands;
        CurrentIndex = 0;
        selectedCommand = Commands.FirstOrDefault();
    }

    public bool SaveAllowed => ParentVm.SaveAllowed;

    /// <summary>
    ///     <see cref="CommandVm_Base.Commands" />
    /// </summary>
    public ObservableCollection<Command> Commands => ParentVm.Commands ?? [];

    /// <summary>
    ///     
    /// </summary>
    public IRelayCommand AddCommand => ParentVm.AddCommand;

    /// <summary>
    ///     
    /// </summary>
    public IRelayCommand SaveCommand => ParentVm.SaveCommand;

    public bool OnExit()
    {
        return Commands.All(c => c.ItemStatus == ItemStatus.None);
    }

    public void OnEnter()
    {
    }

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

    [RelayCommand]
    private void MoveIndexUp()
    {
        if (CurrentIndex > 0)
        {
            CurrentIndex -= 1;
        }
    }

    [RelayCommand]
    private void MoveIndexDown()
    {
        if (CurrentIndex < Commands.Count)
        {
            CurrentIndex += 1;
        }
    }

    [RelayCommand]
    private void ActivateSearch()
    {
        ForbidShortcutExecution = true;
    }

    [RelayCommand]
    private void ExecuteEdit()
    {
        if (SelectedCommand != null)
        {
            ParentVm.EditCommand.Execute(SelectedCommand);
        }
    }

    [RelayCommand]
    private void ExecuteDelete()
    {
        if (SelectedCommand != null)
        {
            ParentVm.DeleteCommand.Execute(SelectedCommand);
        }
    }
}