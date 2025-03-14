using System.IO;
using System.Windows;
using Newtonsoft.Json;

namespace CSlickRun.Logic;

/// <summary>
///    Klasse zum Verwalten der Commands
/// </summary>
public class CommandManager
{
    private List<Command> _defaultCommands = new();

    /// <summary>
    /// Alle Commands
    /// </summary>
    private List<Command> _userCommands = new();

    /// <summary>
    /// Erstellt die Default Commands als Json
    /// </summary>
    /// <returns>Json string</returns>
    public string CreateDefaultCommandsAsJson()
    {
        return JsonConvert.SerializeObject(Global.DefaultCommands);
    }

    /// <summary>
    /// Erstellt die Default Commands
    /// </summary>
    /// <returns>Liste an Default Commands</returns>
    public List<Command> CreateDefaultCommands()
    {
        return Global.DefaultCommands;
    }

    /// <summary>
    /// Gibt alle Commands sortiert zurück
    /// </summary>
    /// <returns></returns>
    public List<Command> GetUserCommands()
    {
        return _userCommands.OrderBy(command => command.Name).ToList();
    }

    public List<Command> GetAllCommands()
    {
        var allCommands = new List<Command>();
        allCommands.AddRange(_userCommands);
        allCommands.AddRange(_defaultCommands);
        return allCommands.OrderBy(command => command.Name).ToList();
    }

    /// <summary>
    /// Setzt die Commands
    /// </summary>
    /// <param name="value">Neue Liste an Commands</param>
    public void SetUserCommands(IEnumerable<Command> value)
    {
        _userCommands = value.OrderBy(command => command.Name).ToList();
    }

    /// <summary>
    /// Lädt die Commands und speichert sie danach
    /// </summary>
    public async Task LoadCommands()
    {
        var commandsAsJson = await File.ReadAllTextAsync(Global.CommandsFile);
        _userCommands = JsonConvert.DeserializeObject<List<Command>>(commandsAsJson) ?? [];
        _defaultCommands = Global.DefaultCommands;
        await SaveCommands();
    }

    /// <summary>
    /// Speichert die Commands
    /// </summary>
    public async Task SaveCommands()
    {
        var commandsAsJson = JsonConvert.SerializeObject(_userCommands);
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