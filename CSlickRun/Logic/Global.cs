using System.IO;
using System.Windows;
using CSlickRun.UI.Windows;

namespace CSlickRun.Logic;

/// <summary>
/// Globale Klasse
/// </summary>
public class Global
{
    public static string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    public static string ConfigPath = Path.Combine(AppDataPath, "CSlickRun");
    public static string ConfigFile = Path.Combine(ConfigPath, "config.json");
    public static string CommandsFile = Path.Combine(ConfigPath, "commands.json");
    public static string HistoryFile = Path.Combine(ConfigPath, "history.json");
    public static string IconFile = Path.Combine(Environment.CurrentDirectory, @"\Design\CSlickRun.ico");
    public static string ImagePath = Path.Combine(Environment.CurrentDirectory, @"\Design\Icons\");


    /// <summary>
    /// Globaler Command-Manager
    /// </summary>
    public static CommandManager GlobalCommandManager = new();

    /// <summary>
    /// Globale Settings
    /// </summary>
    public static Settings GlobalSettings = new();

    /// <summary>
    /// Standard-Commands
    /// </summary>
    public static List<Command> DefaultCommands = new()
    {
        new Command("Config", null, null, true),
        new Command("Exit", null, null, true)
    };

    /// <summary>
    /// Globaler Keyboardhook
    /// </summary>
    public static KeyboardHook GlobalHook = new();

    /// <summary>
    /// Registriert den globalen Shortcut 
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="AggregateException"></exception>
    public static void RegisterGlobalHotkey()
    {
        GlobalHook.RegisterHotkey(
            (CommandLineWindow)Application.Current.MainWindow ?? throw new ArgumentNullException(),
            GlobalSettings.ShortCutCodes ?? throw new ArgumentNullException());
    }

    /// <summary>
    /// Entfernt den momentan registrierten Shortcut
    /// </summary>
    public static void UnregisterGlobalHotkey()
    {
        GlobalHook.UnregisterHotkey();
    }

    /// <summary>
    /// Entfernt den momentan registrierten Shortcut und weist diesen erneut zu
    /// </summary>
    public static void ResetGlobalHotkey()
    {
        UnregisterGlobalHotkey();
        RegisterGlobalHotkey();
    }
}