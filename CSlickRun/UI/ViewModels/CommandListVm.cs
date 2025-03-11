using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CSlickRun.Logic;
using CSlickRun.UI.ViewModels.Base;

namespace CSlickRun.UI.ViewModels;

public partial class CommandListVm : ViewModelBase
{
    [ObservableProperty] private int? _currentIndex;

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(FilterCommandsCommand))]
    private string? commandFilter;

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
        selectedCommand = Commands.FirstOrDefault();
    }

    /// <summary>
    ///     <see cref="CommandVm_Base.Commands" />
    /// </summary>
    public ObservableCollection<Command> Commands => ParentVm.Commands ?? [];


    /// <summary>
    ///     
    /// </summary>
    public ICommand EditCommand => ParentVm.EditCommand;

    /// <summary>
    ///    
    /// </summary>
    public ICommand DeleteCommand => ParentVm.DeleteCommand;

    /// <summary>
    ///     
    /// </summary>
    public ICommand AddCommand => ParentVm.AddCommand;

    /// <summary>
    ///     
    /// </summary>
    public ICommand SaveCommand => ParentVm.SaveCommand;

    [RelayCommand]
    private void MoveIndexUp()
    {
        if (CurrentIndex < Commands.Count)
        {
            CurrentIndex += 1;
        }
    }

    [RelayCommand]
    private void MoveIndexDown()
    {
        if (CurrentIndex >= 0)
        {
            CurrentIndex += 1;
        }
    }

    [RelayCommand]
    private void FilterCommands()
    {
        VisibleCommands = Commands.Where( .Where(x => CommandFilter != null && x.Name.Contains(CommandFilter, StringComparison.OrdinalIgnoreCase)).ToList();)
    }
}