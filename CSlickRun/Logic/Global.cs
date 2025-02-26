using System.IO;

namespace CSlickRun.Logic;

public class Global
{
    public static string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    public static string ConfigPath = Path.Combine(AppDataPath, "CSlickRun");
    public static string ConfigFile = Path.Combine(ConfigPath, "config.json");
    public static string CommandsFile = Path.Combine(ConfigPath, "commands.json");
    public static string HistoryFile = Path.Combine(ConfigPath, "history.json");

    public static CommandManager GlobalCommandManager = new();

    public static Settings GlobalSettings = new();

    public static List<Command> DefaultCommands = new()
    {
        new Command("Config", null, null)
    };

    public static KeyboardHook GlobalHook = new();
}