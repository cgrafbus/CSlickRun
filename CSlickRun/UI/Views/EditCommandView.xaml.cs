using System.Windows;
using CSlickRun.Logic;
using CSlickRun.UI.ViewModels;

namespace CSlickRun.UI.Views;

/// <summary>
///     Interaction logic for EditCommandView.xaml
/// </summary>
public partial class EditCommandView : ViewBase
{
    private readonly EditCommandVm _currentVm;

    public EditCommandView(Command command, CommandVm parentVm)
    {
        InitializeComponent();
        DataContext = new EditCommandVm(parentVm, command, out _currentVm);
        TextBoxName.Focus();
    }

    private void DataGridCommands_OnIsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        _currentVm.PathGridFocused = (bool)e.NewValue;
    }

    private void DataGridCommands_OnGotFocus(object sender, RoutedEventArgs e)
    {
        _currentVm.PathGridFocused = true;
    }

    private void DataGridCommands_OnLostFocus(object sender, RoutedEventArgs e)
    {
        _currentVm.PathGridFocused = false;
    }
}