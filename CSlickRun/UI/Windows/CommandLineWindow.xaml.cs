using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using CSlickRun.Logic;
using CSlickRun.UI.ViewModels;

namespace CSlickRun.UI.Windows;

/// <summary>
///     Interaction logic for CommandLineWindow.xaml
/// </summary>
public partial class CommandLineWindow : Window
{
    /// <summary>
    /// Konstruktor
    /// </summary>
    public CommandLineWindow()
    {
        InitializeComponent();
        Loaded += OnLoaded;
        Closed += OnClosed;
    }

    /// <summary>
    /// OnClosed-EventHandler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnClosed(object? sender, EventArgs e)
    {
        Global.UnRegisterGlobalHotkey();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        Global.RegisterGlobalHotkey();
        Global.GlobalHook.HotkeyPressed += SetCommandStatusAvailable;
    }


    private void CommandLineHost_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        SetCommandStatusAvailable();
    }

    private void SetCommandStatusAvailable()
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            Activate();
            CommandLineHost.BorderBrush = UIHelper.ConvertHexToBrush(((CommandLineVm)DataContext).BorderColor) ??
                                          throw new InvalidOperationException();
            PreviewTextBlock.Visibility = Visibility.Collapsed;
            CommandTextBox.Visibility = Visibility.Visible;
            AutoCompleteTextBlock.Visibility = Visibility.Visible;
            Keyboard.ClearFocus();
            CommandTextBox.Focus();
            Keyboard.Focus(CommandTextBox);
            CommandTextBox.SelectAll();
        });
    }

    private void SetCommandStatusUnAvailable()
    {
        CommandLineHost.BorderBrush = UIHelper.ConvertHexToBrush(((CommandLineVm)DataContext).BorderColor) ??
                                      throw new InvalidOperationException();
        PreviewTextBlock.Visibility = Visibility.Visible;


        CommandTextBox.Visibility = Visibility.Collapsed;
        CommandTextBox.Text = string.Empty;

        AutoCompleteTextBlock.Visibility = Visibility.Collapsed;
        AutoCompleteTextBlock.Text = string.Empty;
    }

    private void CommandTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var preview = Global.GlobalCommandManager.UserCommands
            .FirstOrDefault(c => c.Name.StartsWith(CommandTextBox.Text, StringComparison.OrdinalIgnoreCase));
        if (CommandTextBox.Text == string.Empty || string.IsNullOrWhiteSpace(CommandTextBox.Text))
        {
            preview = null;
        }

        if (preview != null)
        {
            AutoCompleteTextBlock.Text = preview.Name;
            AutoCompleteTextBlock.Inlines.Clear();

            AutoCompleteTextBlock.Inlines.Add(new Run(CommandTextBox.Text)
            {
                Foreground = new SolidColorBrush(Colors.Transparent)
            });


            var suggestionRun = new Run(preview.Name[CommandTextBox.Text.Length..])
            {
                Foreground = UIHelper.ConvertHexToBrush(((CommandLineVm)DataContext).AutoCompleteForegroundColor),
                Background =
                    UIHelper.ConvertHexToBrush(((CommandLineVm)DataContext)
                        .AutoCompleteBackgroundColor) // Hintergrund setzen
            };

            AutoCompleteTextBlock.Inlines.Add(suggestionRun);
        }
        else
        {
            AutoCompleteTextBlock.Text = string.Empty;
        }
    }

    private void CommandTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
        SetCommandStatusUnAvailable();
    }

    private void CommandTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
        {
            return;
        }

        Global.GlobalCommandManager.ExecuteCommand(Global.GlobalCommandManager.UserCommands.FirstOrDefault(c =>
            c.Name.Contains(CommandTextBox.Text, StringComparison.OrdinalIgnoreCase)));
        SetCommandStatusUnAvailable();
    }
}