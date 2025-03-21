using System.Windows;
using System.Windows.Controls;

namespace CSlickRun.UI;

/// <summary>
/// Provides attached properties to manage focus behavior for DataGrid cells.
/// </summary>
public static class GridFocusBehaviour
{
    /// <summary>
    /// DependencyProperty for managing the focus state of a DataGrid cell.
    /// </summary>
    public static readonly DependencyProperty IsFocusedProperty =
        DependencyProperty.RegisterAttached(
            nameof(DataGridCell.IsFocused),
            typeof(bool),
            typeof(GridFocusBehaviour),
            new FrameworkPropertyMetadata(false));

    /// <summary>
    /// Gets the value of the IsFocused attached property.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <returns>The focus state.</returns>
    public static bool GetIsFocused(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsFocusedProperty);
    }

    /// <summary>
    /// Sets the value of the IsFocused attached property.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="value">The focus state.</param>
    public static void SetIsFocused(DependencyObject obj, bool value)
    {
        obj.SetValue(IsFocusedProperty, value);
    }
}