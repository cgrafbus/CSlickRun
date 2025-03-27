using CSlickRun.UI.ViewModels;
using System.Windows;

namespace CSlickRun.UI.Views;

/// <summary>
/// Interaction logic for CommandListView.xaml
/// </summary>
public partial class CommandListView : ViewBase
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

    /// <inheritdoc />
    protected override void InitializeKeyPressBehaviours()
    {
        base.InitializeKeyPressBehaviours();
        SearchTextBoxPlaceholder.Text += " (X)";
    }
}