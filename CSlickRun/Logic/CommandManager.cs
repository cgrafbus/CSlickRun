using System.IO;
using System.Windows;
using Newtonsoft.Json;

namespace CSlickRun.Logic;

public class CommandManager
{
    public List<Command>? UserCommands = new();

    public string CreateDefaultCommandsAsJson()
    {
        var CommandList = new List<Command>();
        CommandList.Add(new Command("Config", null, null));
        return JsonConvert.SerializeObject(CommandList);
    }

    public async Task LoadCommands()
    {
        var commandsAsJson = await File.ReadAllTextAsync(Global.CommandsFile);
        UserCommands = JsonConvert.DeserializeObject<List<Command>>(commandsAsJson) ?? [];
    }

    public async Task SaveCommands()
    {
        var commandsAsJson = JsonConvert.SerializeObject(UserCommands);
        await File.WriteAllTextAsync(Global.CommandsFile, commandsAsJson);
    }

    public void AddCommand(Command command)
    {
        UserCommands?.Add(command);
    }

    public void RemoveCommand(Command command)
    {
        UserCommands?.Remove(command);
    }

    public void UpdateCommand(Command command)
    {
        var index = UserCommands?.FindIndex(c => c.Name == command.Name);
        if (index != null)
        {
            UserCommands![index.Value] = command;
        }
    }

    public void ExecuteCommand(Command? command)
    {
        try
        {
            command.Execute();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Command {command} could not be executed, {ex.Message}", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}