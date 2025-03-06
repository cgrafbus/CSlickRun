using System.Windows.Controls;
using System.Windows.Input;
using CSlickRun.Logic;
using CSlickRun.UI.ViewModels;

namespace CSlickRun.UI.Views;

/// <summary>
///     Interaction logic for CommandListView.xaml
/// </summary>
public partial class CommandListView : UserControl
{
    private CommandVm _currentVm;

    /// <summary>
    /// Konstruktor
    /// </summary>
    /// <param name="viewModel">Überliegendes ViewModel</param>
    public CommandListView(CommandVm viewModel)
    {
        InitializeComponent();
        _currentVm = viewModel;
        DataContext = new CommandListVm(_currentVm);
    }

    private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var itemssource = ((CommandListVm)DataContext).Commands;
        CommandHost.ItemsSource = itemssource.Where(x => x.Name.Contains(SearchTermTextBox.Text, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    private void UserControl_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        var textBoxFocused = SearchTermTextBox.IsFocused;
        if (textBoxFocused)
        {
            return;
        }
        if (e.Key == Key.X)
        {
            SearchTermTextBox.Focus();
            e.Handled = true;
            return;
        }

        if (e.Key == Key.S)
        {
            CommandHost.Focus();
            CommandHost.SelectedIndex += 1;
        }
        if (e.Key == Key.W)
        {
            CommandHost.Focus();
            if (CommandHost.SelectedIndex > -1)
            {
                CommandHost.SelectedIndex -= 1;
            }
        }
    }

    private void ContentGrid_KeyDown(object sender, KeyEventArgs e)
    {
        var selectedItem = CommandHost.SelectedItem;
        if (e.Key == Key.E)
        {
            _currentVm.EditCommand.Execute(selectedItem);
        }
        if (e.Key == Key.Q)
        {
            _currentVm.DeleteCommand.Execute(selectedItem);
        }
    }
}