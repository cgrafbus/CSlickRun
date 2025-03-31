using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using CSlickRun.Logic;

namespace CSlickRun.UI.Controls;

/// <summary>
/// Interaction logic for CommandListButton.xaml
/// </summary>
public partial class CommandListButton
{
    public static readonly DependencyProperty EditCommandProperty = DependencyProperty.Register(
        nameof(EditCommand),
        typeof(ICommand),
        typeof(CommandListButton));

    public static readonly DependencyProperty DeleteCommandProperty = DependencyProperty.Register(
        nameof(DeleteCommand),
        typeof(ICommand),
        typeof(CommandListButton));

    /// <summary>
    /// Constructor
    /// </summary>
    public CommandListButton()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    /// <summary>
    /// Loaded event handler
    /// </summary>
    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (!Global.GlobalSettings.UseKeyPressBehaviour)
        {
            return;
        }
        if (DeleteButton.Content.ToString()?.Contains(" (Q)") == false)
        {
            DeleteButton.Content += " (Q)";
        }
    }

    public ICommand EditCommand
    {
        get => (ICommand)GetValue(EditCommandProperty);
        set => SetValue(EditCommandProperty, value);
    }

    public ICommand DeleteCommand
    {
        get => (ICommand)GetValue(DeleteCommandProperty);
        set => SetValue(DeleteCommandProperty, value);
    }
}