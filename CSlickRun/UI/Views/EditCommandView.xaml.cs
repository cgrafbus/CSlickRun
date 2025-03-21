using System.Windows;
using CSlickRun.Logic;
using CSlickRun.UI.ViewModels;

namespace CSlickRun.UI.Views;

/// <summary>
/// Interaction logic for EditCommandView.xaml
/// </summary>
public partial class EditCommandView
{
    private readonly EditCommandVm _currentVm;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="command">The command to be edited.</param>
    /// <param name="parentVm">The parent ViewModel.</param>
    public EditCommandView(Command command, CommandVm parentVm)
    {
        InitializeComponent();
        DataContext = new EditCommandVm(parentVm, command, out _currentVm);
        TextBoxName.Focus();
    }

    /// <summary>
    /// Event handler for the IsKeyboardFocusWithinChanged event of the DataGridCommands.
    /// Updates the PathGridFocused property of the ViewModel.
    /// </summary>
    private void DataGridCommands_OnIsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        _currentVm.PathGridFocused = (bool)e.NewValue;
    }

    /// <summary>
    /// Event handler for the GotFocus event of the DataGridCommands.
    /// Sets the PathGridFocused property of the ViewModel to true.
    /// </summary>
    private void DataGridCommands_OnGotFocus(object sender, RoutedEventArgs e)
    {
        _currentVm.PathGridFocused = true;
    }

    /// <summary>
    /// Event handler for the LostFocus event of the DataGridCommands.
    /// Sets the PathGridFocused property of the ViewModel to false.
    /// </summary>
    private void DataGridCommands_OnLostFocus(object sender, RoutedEventArgs e)
    {
        _currentVm.PathGridFocused = false;
    }
}

