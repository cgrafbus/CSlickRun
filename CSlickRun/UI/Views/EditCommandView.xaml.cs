using CSlickRun.Logic;
using CSlickRun.UI.ViewModels;

namespace CSlickRun.UI.Views;

/// <summary>
///     Interaction logic for EditCommandView.xaml
/// </summary>
public partial class EditCommandView : ViewBase
{
    private readonly Command _command;
    private readonly EditCommandVm _currentVm;
    private readonly CommandVm _parentVm;

    public EditCommandView(Command command, CommandVm parentVm)
    {
        InitializeComponent();
        _command = command;
        _parentVm = parentVm;
        DataContext = new EditCommandVm(_parentVm, _command);
        _currentVm = DataContext as EditCommandVm ?? throw new InvalidOperationException();
    }
}