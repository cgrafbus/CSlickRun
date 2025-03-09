using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CSlickRun.Logic;
using CSlickRun.UI.ViewModels.Base;

namespace CSlickRun.UI.ViewModels;

public partial class CommandListVm : ViewModelBase
{
    /// <summary>
    ///     Parent-ViewModel
    /// </summary>
    [ObservableProperty] private CommandVm parentVm;

    /// <summary>
    ///     Momentan ausgewählter Befehl
    /// </summary>
    [ObservableProperty] private Command? selectedCommand;


    /// <summary>
    ///     Konstruktor
    /// </summary>
    /// <param name="parentVm">Parent-ViewModel</param>
    public CommandListVm(CommandVm parentvm)
    {
        parentVm = parentvm;
        selectedCommand = Commands.FirstOrDefault();
    }

    /// <summary>
    ///     <see cref="CommandVm_Base.Commands" />
    /// </summary>
    public ObservableCollection<Command> Commands => parentVm.Commands ?? [];


    /// <summary>
    ///     <see cref="CommandVm_Base.EditCommand" />
    /// </summary>
    public ICommand EditCommand => parentVm.EditCommand;

    /// <summary>
    ///     <see cref="CommandVm_Base.DeleteCommand" />
    /// </summary>
    public ICommand DeleteCommand => parentVm.DeleteCommand;

    /// <summary>
    ///     <see cref="CommandVm_Base.AddCommand" />
    /// </summary>
    public ICommand AddCommand => parentVm.AddCommand;

    /// <summary>
    ///     <see cref="CommandVm_Base.SaveCommand" />
    /// </summary>
    public ICommand SaveCommand => parentVm.SaveCommand;

    [RelayCommand]
    private void Test()
    {
        MessageBox.Show("Test");
    }
}