using CSlickRun.Logic;
using CSlickRun.UI.ViewModels;

namespace CSlickRun.UI.Views;

/// <summary>
///     Interaction logic for EditCommandView.xaml
/// </summary>
public partial class EditCommandView : ViewBase
{

    public EditCommandView(Command command, CommandVm parentVm)
    {
        InitializeComponent();
        DataContext = new EditCommandVm(parentVm, command);
        TextBoxName.Focus();
    }
}