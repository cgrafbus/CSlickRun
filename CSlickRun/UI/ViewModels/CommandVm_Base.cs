using CSlickRun.Logic;
using CSlickRun.UI.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace CSlickRun.UI.ViewModels;

public class CommandVm_Base : ViewModelBase
{
    public ObservableCollection<Command> Commands { get; set; } = new();

    private UserControl _currentCommandView;
    public UserControl CurrentCommandView
    {
        get => _currentCommandView;
        set => SetField(ref _currentCommandView, value);
    }

    public  RelayCommand EditCommand { get; set; }
    public RelayCommand ShowCommandList { get; set; }
}