using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using CSlickRun.Logic;
using Microsoft.VisualBasic;
using Microsoft.Xaml.Behaviors;
using Interaction = Microsoft.Xaml.Behaviors.Interaction;

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
    }

    /// <summary>
    /// Initializes the key press behaviours.
    /// </summary>
    protected virtual void InitializeKeyPressBehaviours()
    {
        foreach (var behaviour in Interaction.GetBehaviors(this))
        {
            if (behaviour is KeyPressBehavior keyb)
            {
                keyb.Enabled = true;
                UIHelper.ChangeControlContentsByKeyPressBehavior(keyb, this);
            }
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
        foreach (var element in UIHelper.FindAllChildren(this))
        {
            element.LostFocus += ElementOnLostFocus;
        }
        if (Global.GlobalSettings.UseKeyPressBehaviour)
        {
            InitializeKeyPressBehaviours();
        }
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