using System.IO;
using System.Windows;
using CSlickRun.UI.Windows;

namespace CSlickRun.Logic;

/// <summary>
/// Global Class
/// </summary>
public class Global
{
    public static string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    public static string ConfigPath = Path.Combine(AppDataPath, "CSlickRun");
    public static string ConfigFile = Path.Combine(ConfigPath, "config.json");
    public static string CommandsFile = Path.Combine(ConfigPath, "commands.json");
    public static string HistoryFile = Path.Combine(ConfigPath, "history.txt");
    public static string IconFile = Path.Combine(Environment.CurrentDirectory, @"\Design\CSlickRun.ico");
    public static string ImagePath = Path.Combine(Environment.CurrentDirectory, @"\Design\Icons\");


    /// <summary>
    /// Global Command-Manager
    /// </summary>
    public static CommandFactory GlobalCommandManager = new();

    /// <summary>
    /// Global Settings
    /// </summary>
    public static Settings GlobalSettings = new();

    /// <summary>
    /// Default-Commands
    /// </summary>
    public static List<Command> DefaultCommands =
    [
        new("Config", null, null, true),
        new("Exit", null, null, true)
    ];

    /// <summary>
    /// Global Keyboardhook
    /// </summary>
    public static KeyboardHook GlobalHook = new();

    /// <summary>
    /// Registers the global Shortcut
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="AggregateException"></exception>
    public static void RegisterGlobalHotkey()
    {
        GlobalHook.RegisterHotkey(
            (CommandLineWindow?)Application.Current.MainWindow ?? throw new ArgumentNullException(),
            GlobalSettings.ShortCutCodes ?? throw new ArgumentNullException());
    }

    /// <summary>
    /// Removes the global Shortcut
    /// </summary>
    public static void UnregisterGlobalHotkey()
    {
        GlobalHook.UnregisterHotkey();
    }

    /// <summary>
    /// Resets the global Shortcut
    /// </summary>
    public static void ResetGlobalHotkey()
    {
        UnregisterGlobalHotkey();
        RegisterGlobalHotkey();
    }
}