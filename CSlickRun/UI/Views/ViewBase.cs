using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CSlickRun.UI.Views;

public class ViewBase : UserControl
{
    public ViewBase()
    {
        Loaded += OnLoaded;

        foreach (var element in UIHelper.FindAllChildren(this))
        {
            element.LostFocus += ElementOnLostFocus;
        }
    }

    private void ElementOnLostFocus(object sender, RoutedEventArgs e)
    {
        Application.Current.Dispatcher.Invoke(FocusThing);
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        Application.Current.Dispatcher.Invoke(FocusThing);
    }

    private void FocusThing()
    {
        Focus();
        Keyboard.ClearFocus();
        Keyboard.Focus(this);
    }
}