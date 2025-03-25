using System.Windows;
using System.Windows.Controls;
using CSlickRun.Logic;

namespace CSlickRun.UI.Controls;

public class CListBox : ListBox
{
    public static readonly DependencyProperty BindableSelectedItemsProperty =
        DependencyProperty.Register(nameof(BindableSelectedItems),
            typeof(IList<Command>), typeof(CListBox),
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBindableSelectedItemsChanged));

    public IList<Command> BindableSelectedItems
    {
        get => (IList<Command>)GetValue(BindableSelectedItemsProperty);
        set => SetValue(BindableSelectedItemsProperty, value);
    }

    protected override void OnSelectionChanged(SelectionChangedEventArgs e)
    {
        base.OnSelectionChanged(e);
        BindableSelectedItems = SelectedItems.Cast<Command>().ToList();
    }

    private static void OnBindableSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is CListBox listBox)
        {
            listBox.SetSelectedItems(listBox.BindableSelectedItems);
        }
    }
}