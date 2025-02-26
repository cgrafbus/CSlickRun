using System.Collections.ObjectModel;
using System.Windows.Controls;
using CSlickRun.Logic;
using CSlickRun.UI.ViewModels.Base;

namespace CSlickRun.UI.ViewModels;

public class CommandVm_Base : ViewModelBase
{
    private ObservableCollection<Command> _commands;
    private UserControl _currentCommandView;

    public UserControl CurrentCommandView
    {
        get => _currentCommandView;
        set => SetField(ref _currentCommandView, value);
    }

    public ObservableCollection<Command> Commands
    {
        get => _commands;
        set => SetField(ref _commands, value);
    }


    public RelayCommand EditCommand { get; set; }
    public RelayCommand AddCommand { get; set; }
    public RelayCommand DeleteCommand { get; set; }
    public RelayCommand ShowCommandsList { get; set; }
    public AsyncRelayCommand SaveCommand { get; set; }
}