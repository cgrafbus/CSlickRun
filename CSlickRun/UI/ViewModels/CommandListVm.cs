using System.Collections.ObjectModel;
using System.Windows.Input;
using CSlickRun.Logic;
using CSlickRun.UI.ViewModels.Base;

namespace CSlickRun.UI.ViewModels;

public class CommandListVm : ViewModelBase
{
    /// <summary>
    /// Parent-ViewModel
    /// </summary>
    private readonly CommandVm _parentVm;

    /// <summary>
    /// Momentan ausgewählter Befehl
    /// </summary>
    private Command? _selectedCommand;


    /// <summary>
    /// Konstruktor
    /// </summary>
    /// <param name="parentVm">Parent-ViewModel</param>
    public CommandListVm(CommandVm parentVm)
    {
        _parentVm = parentVm;
        _selectedCommand = Commands.FirstOrDefault();
    }

    /// <summary>
    /// <see cref="CommandVm_Base.Commands"/>
    /// </summary>
    public ObservableCollection<Command> Commands => _parentVm.Commands ?? [];

    /// <summary>
    /// <see cref="_selectedCommand"/>
    /// </summary>
    public Command? SelectedCommand
    {
        get => _selectedCommand;
        set => SetField(ref _selectedCommand, value);
    }

    /// <summary>
    /// <see cref="CommandVm_Base.EditCommand"/>
    /// </summary>
    public ICommand EditCommand => _parentVm.EditCommand;

    /// <summary>
    /// <see cref="CommandVm_Base.DeleteCommand"/>
    /// </summary>
    public ICommand DeleteCommand => _parentVm.DeleteCommand;

    /// <summary>
    /// <see cref="CommandVm_Base.AddCommand"/>
    /// </summary>
    public ICommand AddCommand => _parentVm.AddCommand;

    /// <summary>
    /// <see cref="CommandVm_Base.SaveCommand"/>
    /// </summary>
    public ICommand SaveCommand => _parentVm.SaveCommand;
}