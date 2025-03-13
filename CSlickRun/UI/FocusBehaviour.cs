using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace CSlickRun.UI;

public static class FocusBehaviour
{
    public static readonly DependencyProperty IsFocusedProperty =
        DependencyProperty.RegisterAttached(
            "IsFocused",
            typeof(bool),
            typeof(FocusBehaviour),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnIsFocusedChanged));

    public static readonly DependencyProperty TrackFocusProperty =
        DependencyProperty.RegisterAttached(
            "TrackFocus",
            typeof(bool),
            typeof(FocusBehaviour),
            new PropertyMetadata(false, OnTrackFocusChanged));

    public static bool GetIsFocused(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsFocusedProperty);
    }

    public static void SetIsFocused(DependencyObject obj, bool value)
    {
        obj.SetValue(IsFocusedProperty, value);
    }

    private static void OnIsFocusedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Control element && e.NewValue is true)
        {
            element.Focus();
        }
    }

    public static bool GetTrackFocus(DependencyObject obj)
    {
        return (bool)obj.GetValue(TrackFocusProperty);
    }

    public static void SetTrackFocus(DependencyObject obj, bool value)
    {
        obj.SetValue(TrackFocusProperty, value);
    }

    private static void OnTrackFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Control textBox && (bool)e.NewValue)
        {
            textBox.GotFocus += (s, _) => SetIsFocused((Control)s, true);
            textBox.LostFocus += (s, _) => SetIsFocused((Control)s, false);
        }
    }
}