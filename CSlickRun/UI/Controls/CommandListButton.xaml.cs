using System.Windows.Controls;
using System.Windows.Data;

namespace CSlickRun.UI.Controls;

/// <summary>
/// Interaction logic for CommandListButton.xaml
/// </summary>
public partial class CommandListButton : UserControl
{
    public CommandListButton()
    {
        InitializeComponent();
    }

    private void FrameworkElement_OnSourceUpdated(object? sender, DataTransferEventArgs e)
    {
        UpdateLayout();
    }
}