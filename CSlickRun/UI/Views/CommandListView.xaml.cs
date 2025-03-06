using System.Windows.Controls;
using CSlickRun.Logic;
using CSlickRun.UI.ViewModels;

namespace CSlickRun.UI.Views;

/// <summary>
///     Interaction logic for CommandListView.xaml
/// </summary>
public partial class CommandListView : UserControl
{
    /// <summary>
    /// Konstruktor
    /// </summary>
    /// <param name="viewModel">Überliegendes ViewModel</param>
    public CommandListView(CommandVm viewModel)
    {
        InitializeComponent();
        DataContext = new CommandListVm(viewModel);
    }

    private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var itemssource = ((CommandListVm)DataContext).Commands;
        CommandHost.ItemsSource = itemssource.Where(x => x.Name.Contains(SearchTextBox.Text, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}