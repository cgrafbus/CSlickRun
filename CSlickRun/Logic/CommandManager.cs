using System.IO;
using System.Windows;
using Newtonsoft.Json;

namespace CSlickRun.Logic;

/// <summary>
///    Klasse zum Verwalten der Commands
/// </summary>
public class CommandManager
{
    /// <summary>
    /// Alle Commands
    /// </summary>
    public List<Command> UserCommands = new();

    /// <summary>
    /// Erstellt die Default Commands als Json
    /// </summary>
    /// <returns>Json string</returns>
    public string CreateDefaultCommandsAsJson()
    {
        var CommandList = CreateDefaultCommands();
        return JsonConvert.SerializeObject(CommandList);
    }

    /// <summary>
    /// Erstellt die Default Commands
    /// </summary>
    /// <returns>Liste an Default Commands</returns>
    public List<Command> CreateDefaultCommands()
    {
        var CommandList = new List<Command> 
        { 
            new(
            "Config", null, null)
        };
        return CommandList;
    }

    /// <summary>
    /// Lädt die Commands und speichert sie danach
    /// </summary>
    public async Task LoadCommands()
    {
        var commandsAsJson = await File.ReadAllTextAsync(Global.CommandsFile);
        UserCommands = JsonConvert.DeserializeObject<List<Command>>(commandsAsJson) ?? [];
        await SaveCommands();
    }

    /// <summary>
    /// Speichert die Commands
    /// </summary>
    public async Task SaveCommands()
    {
        var defaultCommands = CreateDefaultCommands();
        foreach (var defaultCommand in defaultCommands.Where(defaultCommand => UserCommands.All(c => c.Name != defaultCommand.Name)))
        {
            UserCommands.Add(defaultCommand);
        }

        var commandsAsJson = JsonConvert.SerializeObject(UserCommands);
        await File.WriteAllTextAsync(Global.CommandsFile, commandsAsJson);
    }

    /// <summary>
    /// Führt einen Command aus
    /// </summary>
    /// <param name="command">Auszuführender Command</param>
    public void ExecuteCommand(Command? command)
    {
        try
        {
            command?.Execute();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Command {command} could not be executed, {ex.Message}", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}