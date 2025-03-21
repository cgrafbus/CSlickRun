using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CSlickRun.UI.Views;

/// <summary>
/// Base class for views, providing common functionality.
/// </summary>
public class ViewBase : UserControl
{
    /// <summary>
    /// Constructor
    /// </summary>
    public ViewBase()
    {
        Loaded += OnLoaded;

        foreach (var element in UIHelper.FindAllChildren(this))
        {
            element.LostFocus += ElementOnLostFocus;
        }
    }

    /// <summary>
    /// Handles the LostFocus event for child elements.
    /// </summary>
    private void ElementOnLostFocus(object sender, RoutedEventArgs e)
    {
        Application.Current.Dispatcher.Invoke(FocusThing);
    }

    /// <summary>
    /// Handles the Loaded event.
    /// </summary>
    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        Application.Current.Dispatcher.Invoke(FocusThing);
    }

    /// <summary>
    /// Sets focus to the current element.
    /// </summary>
    private void FocusThing()
    {
        Focus();
        Keyboard.ClearFocus();
        Keyboard.Focus(this);
    }
}