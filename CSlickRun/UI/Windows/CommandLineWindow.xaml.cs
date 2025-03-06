using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using CSlickRun.Logic;
using CSlickRun.UI.ViewModels;

namespace CSlickRun.UI.Windows;

/// <summary>
/// Interaction logic for CommandLineWindow.xaml
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
    /// Event-Handler für das Closed-Ereignis.
    /// </summary>
    /// <param name="sender">Der Ereignis-Sender.</param>
    /// <param name="e">Die Ereignis-Daten.</param>
    private void OnClosed(object? sender, EventArgs e)
    {
        Global.UnregisterGlobalHotkey();
    }

    /// <summary>
    /// Event-Handler für das Loaded-Ereignis.
    /// </summary>
    /// <param name="sender">Der Ereignis-Sender.</param>
    /// <param name="e">Die Ereignis-Daten.</param>
    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        Global.RegisterGlobalHotkey();
        Global.GlobalHook.HotkeyPressed += SetCommandStatusAvailable;
    }

    /// <summary>
    /// Event-Handler für das MouseLeftButtonDown-Ereignis des CommandLineHost.
    /// </summary>
    /// <param name="sender">Der Ereignis-Sender.</param>
    /// <param name="e">Die Ereignis-Daten.</param>
    private void CommandLineHost_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        SetCommandStatusAvailable();
    }

    /// <summary>
    /// Setzt den Status der Befehlszeile auf verfügbar.
    /// </summary>
    private void SetCommandStatusAvailable()
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            Activate();
            ((CommandLineVm)DataContext).CurrentBorderColor = ((CommandLineVm)DataContext).BorderColor;
            ((CommandLineVm)DataContext).CurrentBackgroundColor =
                ((CommandLineVm)DataContext).CommandLineBackgroundColor;
            PreviewTextBlock.Visibility = Visibility.Collapsed;
            CommandTextBox.Visibility = Visibility.Visible;
            AutoCompleteTextBlock.Visibility = Visibility.Visible;
            Keyboard.ClearFocus();
            CommandTextBox.Focus();
            Keyboard.Focus(CommandTextBox);
            CommandTextBox.SelectAll();
        });
    }

    /// <summary>
    /// Setzt den Status der Befehlszeile auf nicht verfügbar.
    /// </summary>
    private void SetCommandStatusUnavailable()
    {
        ((CommandLineVm)DataContext).CurrentBorderColor = ((CommandLineVm)DataContext).BorderInactiveColor;
        ((CommandLineVm)DataContext).CurrentBackgroundColor =
            ((CommandLineVm)DataContext).CommandLineInactiveBackgroundColor;
        PreviewTextBlock.Visibility = Visibility.Visible;
        CommandTextBox.Visibility = Visibility.Collapsed;
        CommandTextBox.Text = string.Empty;
        AutoCompleteTextBlock.Visibility = Visibility.Collapsed;
        AutoCompleteTextBlock.Text = string.Empty;
    }

    /// <summary>
    /// Event-Handler für das TextChanged-Ereignis des CommandTextBox.
    /// </summary>
    /// <param name="sender">Der Ereignis-Sender.</param>
    /// <param name="e">Die Ereignis-Daten.</param>
    private void CommandTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var preview = Global.GlobalCommandManager.GetCommands().FirstOrDefault(c =>
            c.Name.StartsWith(CommandTextBox.Text, StringComparison.OrdinalIgnoreCase));
        if (string.IsNullOrEmpty(CommandTextBox.Text) || string.IsNullOrWhiteSpace(CommandTextBox.Text))
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
                Background = UIHelper.ConvertHexToBrush(((CommandLineVm)DataContext).AutoCompleteBackgroundColor)
            };

            AutoCompleteTextBlock.Inlines.Add(suggestionRun);
        }
        else
        {
            AutoCompleteTextBlock.Text = string.Empty;
        }
    }

    /// <summary>
    /// Event-Handler für das LostKeyboardFocus-Ereignis des CommandTextBox.
    /// </summary>
    /// <param name="sender">Der Ereignis-Sender.</param>
    /// <param name="e">Die Ereignis-Daten.</param>
    private void CommandTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
        SetCommandStatusUnavailable();
        OnDeactivated(null!);
    }

    /// <summary>
    /// Event-Handler für das KeyDown-Ereignis des CommandTextBox.
    /// </summary>
    /// <param name="sender">Der Ereignis-Sender.</param>
    /// <param name="e">Die Ereignis-Daten.</param>
    private void CommandTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
        {
            return;
        }

        Global.GlobalCommandManager.ExecuteCommand(Global.GlobalCommandManager.GetCommands().FirstOrDefault(c =>
            c.Name.Contains(CommandTextBox.Text, StringComparison.OrdinalIgnoreCase)));
        SetCommandStatusUnavailable();
    }
}