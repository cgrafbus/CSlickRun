using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using CSlickRun.UI.Controls;
using CSlickRun.UI.Windows;
using Newtonsoft.Json;

namespace CSlickRun.Logic;

/// <summary>
/// Command-Class
/// </summary>
public class Command
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name">
    /// <inheritdoc cref="Name" />
    /// </param>
    /// <param name="paths">
    /// <inheritdoc cref="Paths" />
    /// </param>
    /// <param name="note">
    /// <inheritdoc cref="Note" />
    /// </param>
    /// <param name="defaultCommand"> <inheritdoc cref="DefaultCommand" /></param>
    public Command(string name, List<CommandPath>? paths, string? note, bool defaultCommand = false)
    {
        Name = name;
        Paths = paths;
        Note = note;
        DefaultCommand = defaultCommand;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public Command()
    {
    }

    /// <summary>
    /// Item-Status of command (only used for UI)
    /// </summary>
    [JsonIgnore]
    public ItemStatus ItemStatus { get; set; }

    [JsonIgnore]
    public ItemStatus OldItemStatus { get; set; }

    /// <summary>
    /// Name of command
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Paths to be executed
    /// </summary>
    public List<CommandPath>? Paths { get; set; }

    /// <summary>
    /// Note to command
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Flag, whether or not command is a default command
    /// </summary>
    public bool DefaultCommand { get; set; }

    /// <summary>
    /// Executes command
    /// </summary>
    public void Execute()
    {
        if (Global.GlobalSettings.WriteHistory)
        {
            WriteHistory();
        }

        if (DefaultCommand)
        {
            CheckAndExecuteDefaultCommands();
            return;
        }

        if (Paths == null)
        {
            return;
        }

        foreach (var path in Paths)
        {
            var process = new Process();
            var info = new ProcessStartInfo
            {
                FileName = path.Path,
                Arguments = path.Argument,
                ErrorDialog = true,
                UseShellExecute = path.StartupPath is null or ""
            };
            if (!info.UseShellExecute)
            {
                info.WorkingDirectory = path.StartupPath;
            }

            process.StartInfo = info;
            process.Start();
        }
    }

    /// <summary>
    /// Writes the command to the history
    /// </summary>
    private void WriteHistory()
    {
        var historyText = $"[{DateTime.Now}] - {Name} ";
        File.AppendAllText(Global.HistoryFile, historyText);
    }

    /// <summary>
    /// Executes Default Commands
    /// </summary>
    private void CheckAndExecuteDefaultCommands()
    {
        switch (Name)
        {
            case "Config":
            {
                var confWindow = Application.Current.Windows
                    .OfType<ConfigWindow>()
                    .FirstOrDefault();

                if (confWindow != null)
                {
                    confWindow.Activate();
                    return;
                }

                var configWindow = new ConfigWindow();
                configWindow.Show();
                Keyboard.ClearFocus();
                Keyboard.Focus(configWindow);
                configWindow.Focus();
                return;
            }
            case "Exit":
                Application.Current.Shutdown();
                return;
            default:
                return;
        }
    }
}