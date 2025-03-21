using System.Windows;
using System.Windows.Controls;

namespace CSlickRun.UI;

/// <summary>
/// Provides attached properties to manage focus behavior for WPF controls.
/// </summary>
public static class FocusBehaviour
{
    /// <summary>
    /// DependencyProperty for managing the focus state of a control.
    /// </summary>
    public static readonly DependencyProperty IsFocusedProperty =
        DependencyProperty.RegisterAttached(
            "IsFocused",
            typeof(bool),
            typeof(FocusBehaviour),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnIsFocusedChanged));

    /// <summary>
    /// DependencyProperty for tracking the focus state of a control.
    /// </summary>
    public static readonly DependencyProperty TrackFocusProperty =
        DependencyProperty.RegisterAttached(
            "TrackFocus",
            typeof(bool),
            typeof(FocusBehaviour),
            new PropertyMetadata(false, OnTrackFocusChanged));

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

    /// <summary>
    /// Called when the IsFocused property changes.
    /// </summary>
    /// <param name="d">The dependency object.</param>
    /// <param name="e">The event data.</param>
    private static void OnIsFocusedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Control element && e.NewValue is true)
        {
            element.Focus();
        }
    }

    /// <summary>
    /// Gets the value of the TrackFocus attached property.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <returns>The track focus state.</returns>
    public static bool GetTrackFocus(DependencyObject obj)
    {
        return (bool)obj.GetValue(TrackFocusProperty);
    }

    /// <summary>
    /// Sets the value of the TrackFocus attached property.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="value">The track focus state.</param>
    public static void SetTrackFocus(DependencyObject obj, bool value)
    {
        obj.SetValue(TrackFocusProperty, value);
    }

    /// <summary>
    /// Called when the TrackFocus property changes.
    /// </summary>
    /// <param name="d">The dependency object.</param>
    /// <param name="e">The event data.</param>
    private static void OnTrackFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Control textBox && (bool)e.NewValue)
        {
            textBox.GotFocus += (s, _) => SetIsFocused((Control)s, true);
            textBox.LostFocus += (s, _) => SetIsFocused((Control)s, false);
        }
    }
}
