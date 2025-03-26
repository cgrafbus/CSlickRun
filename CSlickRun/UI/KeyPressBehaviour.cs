using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace CSlickRun.UI;

/// <summary>
/// A behavior that executes a command when a specific key is pressed.
/// </summary>
public class KeyPressBehavior : Behavior<UIElement>
{
    /// <summary>
    /// DependencyProperty for the command to be executed.
    /// </summary>
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(KeyPressBehavior));

    /// <summary>
    /// DependencyProperty for the key that triggers the command.
    /// </summary>
    public static readonly DependencyProperty KeyProperty =
        DependencyProperty.Register(nameof(Key), typeof(Key), typeof(KeyPressBehavior));

    /// <summary>
    /// DependencyProperty to determine if the command can be executed.
    /// </summary>
    public static readonly DependencyProperty CanExecuteProperty =
        DependencyProperty.Register(nameof(CanExecute), typeof(bool), typeof(KeyPressBehavior));

    /// <summary>
    /// Gets or sets the command to be executed.
    /// </summary>
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    /// Gets or sets the key that triggers the command.
    /// </summary>
    public Key Key
    {
        get => (Key)GetValue(KeyProperty);
        set => SetValue(KeyProperty, value);
    }

    //TODO
    public bool Enabled
    {
        get => (bool)GetValue(EnabledProperty);
        set => SetCurrentValue(EnabledProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the command can be executed.
    /// </summary>
    public bool CanExecute
    {
        get => (bool)GetValue(CanExecuteProperty);
        set => SetCurrentValue(CanExecuteProperty, value);
    }

    //TODO
    public static readonly DependencyProperty EnabledProperty = DependencyProperty.Register(
        nameof(Enabled), typeof(bool), typeof(KeyPressBehavior), new PropertyMetadata(false));

    /// <summary>
    /// Called when the behavior is attached to a UI element.
    /// </summary>
    protected override void OnAttached()
    {
        AssociatedObject.KeyDown += OnKeyDown;
        AssociatedObject.Focusable = true; // Ensure it can receive key events
        AssociatedObject.Focus(); // Try to set focus on load
    }

    /// <summary>
    /// Called when the behavior is detached from a UI element.
    /// </summary>
    protected override void OnDetaching()
    {
        AssociatedObject.KeyDown -= OnKeyDown;
    }

    /// <summary>
    /// Handles the KeyDown event and executes the command if the specified key is pressed.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key || Command?.CanExecute(null) != true || !CanExecute || !Enabled)
        {
            return;
        }

        Command.Execute(null);
        e.Handled = true;
    }
}
