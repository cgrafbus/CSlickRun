using System.IO;
using System.Windows;
using Newtonsoft.Json;

namespace CSlickRun.Logic;

/// <summary>
/// Class to manage commands
/// </summary>
public class CommandManager
{
    /// <summary>
    /// Commands added by the User
    /// </summary>
    private List<Command> _defaultCommands = new();

    /// <summary>
    /// All Commands
    /// </summary>
    private List<Command> _userCommands = new();

    /// <summary>
    /// Gets all User-Commands
    /// </summary>
    /// <returns>List of User-Commands ordered by name</returns>
    public List<Command> GetUserCommands()
    {
        return _userCommands.OrderBy(command => command.Name).ToList();
    }

    /// <summary>
    /// Gets all existing commands
    /// </summary>
    /// <returns>List of all commands sorted by name</returns>
    public List<Command> GetAllCommands()
    {
        var allCommands = new List<Command>();
        allCommands.AddRange(_userCommands);
        allCommands.AddRange(_defaultCommands);
        return allCommands.OrderBy(command => command.Name).ToList();
    }

    /// <summary>
    /// Sets the user-commands
    /// </summary>
    /// <param name="value">Neue Liste an Commands</param>
    public void SetUserCommands(IEnumerable<Command> value)
    {
        _userCommands = value.OrderBy(command => command.Name).ToList();
    }

    /// <summary>
    /// Loads Commands
    /// </summary>
    public async Task LoadCommands()
    {
        var commandsAsJson = await File.ReadAllTextAsync(Global.CommandsFile);
        _userCommands = JsonConvert.DeserializeObject<List<Command>>(commandsAsJson) ?? [];
        _defaultCommands = Global.DefaultCommands;
    }

    /// <summary>
    /// Saves the Commands
    /// </summary>
    public async Task SaveCommands()
    {
        var commandsAsJson = JsonConvert.SerializeObject(_userCommands);
        await File.WriteAllTextAsync(Global.CommandsFile, commandsAsJson);
    }
}