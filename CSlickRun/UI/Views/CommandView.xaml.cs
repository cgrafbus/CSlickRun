using System.Windows.Controls;
using CSlickRun.Logic;
using CSlickRun.UI.Controls;
using CSlickRun.UI.ViewModels;

namespace CSlickRun.UI.Views;

/// <summary>
/// Interaction logic for CommandView.xaml
/// </summary>
public partial class CommandView : ISubView
{
    /// <summary>
    /// Constructor
    /// </summary>
    public CommandView()
    {
        InitializeComponent();
    }

    public bool OnExit()
    {
        return true;
    }

    public void OnEnter()
    {
    }

    public void OnLayoutChanged()
    {
    }

    /// <inheritdoc />
    protected override void InitializeKeyPressBehaviours()
    {
        base.InitializeKeyPressBehaviours();
        SearchTextBoxPlaceholder.Text += " (X)";
    }

    private void CommandHost_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (sender is CListBox lb)
        {
            SelectedCommandGotChanged((Command)lb.SelectedItem);
        }
    }

    private void SelectedCommandGotChanged(Command? comm)
    {
        if (DataContext is CommandVm vm)
        {
            vm.UpdateEditSectionCommand.Execute(comm ?? new Command());
        }
    }

    private void DataGridCommands_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
    {
        if (sender is DataGrid grid)
        {
            foreach (var item in grid.Columns)
            {
                item.Width = grid.Width / grid.Columns.Count;
            }
        }
    }
}