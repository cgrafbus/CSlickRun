using System.Windows;
using System.Windows.Controls;

namespace CSlickRun.UI;

public static class GridFocusBehaviour
{
    public static readonly DependencyProperty IsFocusedProperty =
        DependencyProperty.RegisterAttached(
            nameof(DataGridCell.IsFocused),
            typeof(bool),
            typeof(GridFocusBehaviour),
            new FrameworkPropertyMetadata(false));

    public static bool GetIsFocused(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsFocusedProperty);
    }

    public static void SetIsFocused(DependencyObject obj, bool value)
    {
        obj.SetValue(IsFocusedProperty, value);
    }
}