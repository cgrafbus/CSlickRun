using CSlickRun.UI.ViewModels;

namespace CSlickRun.UI.Views;

/// <summary>
///     Interaction logic for CommandListView.xaml
/// </summary>
public partial class CommandListView : ViewBase
{
    private readonly CommandVm _currentVm;

    /// <summary>
    ///     Konstruktor
    /// </summary>
    /// <param name="viewModel">Überliegendes ViewModel</param>
    public CommandListView(CommandVm viewModel)
    {
        InitializeComponent();
        _currentVm = viewModel;
        DataContext = new CommandListVm(_currentVm);
    }
}