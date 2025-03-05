using System.Windows.Controls;
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
}