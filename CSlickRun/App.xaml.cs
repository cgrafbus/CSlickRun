using System.IO;
using System.Windows;
using System.Windows.Threading;
using CSlickRun.Logic;

namespace CSlickRun;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App
{
    /// <summary>
    /// Konstruktor
    /// </summary>
    public App()
    {
        ConfigureExceptionHandling();
        Global.GlobalCommandManager = new CommandManager();
        CheckConfigs().Wait();
        Global.GlobalSettings.LoadAsync().Wait();
        Global.GlobalCommandManager.LoadCommands().Wait();
    }

    /// <summary>
    /// Überprüft die Konfigurationen
    /// </summary>
    private async Task CheckConfigs()
    {
        if (!Directory.Exists(Global.ConfigPath)) Directory.CreateDirectory(Global.ConfigPath);
        if (!File.Exists(Global.CommandsFile))
        {
            await File.WriteAllTextAsync(Global.CommandsFile, "");
        }

        if (!File.Exists(Global.ConfigFile))
        {
            var defaultSettings = Global.GlobalSettings.GetDefaultSettingsAsJson();
            await File.WriteAllTextAsync(Global.ConfigFile, defaultSettings);
        }

        if (!File.Exists(Global.HistoryFile)) await File.WriteAllTextAsync(Global.HistoryFile, "");
    }

    /// <summary>
    /// Konfiguriert das Exception Handling
    /// </summary>
    private void ConfigureExceptionHandling()
    {
        DispatcherUnhandledException += OnDispatcherUnhandledException;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

        // Catch unobserved task exceptions
        TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
    }

    /// <summary>
    /// Behandelt unobserved Task Exceptions
    /// </summary>
    private void TaskSchedulerOnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        ShowException(e.Exception);
        e.SetObserved(); // Prevents process termination
    }

    /// <summary>
    /// Behandelt unhandled Exceptions
    /// </summary>
    private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        ShowException(new Exception("Unhandled Exception"));
    }

    /// <inheritdoc />
    protected override void OnExit(ExitEventArgs e)
    {
        if (Global.GlobalHook.IsHotkeyRegistered()) Global.GlobalHook.UnregisterHotkey();
        base.OnExit(e);
    }

    /// <summary>
    /// Behandelt unhandled Dispatcher Exceptions
    /// </summary>
    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        ShowException(e.Exception);
        e.Handled = true; // Prevents application crash
    }

    /// <summary>
    /// Zeigt eine Exception an
    /// </summary>
    private void ShowException(Exception ex)
    {
        MessageBox.Show(ex.Message, "Exception Occurred", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}