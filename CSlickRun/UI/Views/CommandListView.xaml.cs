using CSlickRun.UI.ViewModels;

namespace CSlickRun.UI.Views;

/// <summary>
/// Interaction logic for CommandListView.xaml
/// </summary>
public partial class CommandListView
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="viewModel">Parent-ViewModell</param>
    public CommandListView(CommandVm viewModel)
    {
        InitializeComponent();
        DataContext = new CommandListVm(viewModel);
    }
}