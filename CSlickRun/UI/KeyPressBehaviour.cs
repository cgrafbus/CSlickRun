using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace CSlickRun.UI;

public class KeyPressBehavior : Behavior<UIElement>
{
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(KeyPressBehavior));

    public static readonly DependencyProperty KeyProperty =
        DependencyProperty.Register(nameof(Key), typeof(Key), typeof(KeyPressBehavior));

    public static readonly DependencyProperty CanExecuteProperty =
        DependencyProperty.Register(nameof(CanExecute), typeof(bool), typeof(KeyPressBehavior));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public Key Key
    {
        get => (Key)GetValue(KeyProperty);
        set => SetValue(KeyProperty, value);
    }

    public bool CanExecute
    {
        get => (bool)GetValue(CanExecuteProperty);
        set => SetCurrentValue(CanExecuteProperty, value);
    }

    protected override void OnAttached()
    {
        AssociatedObject.KeyDown += OnKeyDown;
        AssociatedObject.Focusable = true; // Ensure it can receive key events
        AssociatedObject.Focus(); // Try to set focus on load
    }

    protected override void OnDetaching()
    {
        AssociatedObject.KeyDown -= OnKeyDown;
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key || Command?.CanExecute(null) != true || !CanExecute)
        {
            return;
        }

        Command.Execute(null);
        e.Handled = true;
    }
}