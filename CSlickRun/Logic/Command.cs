using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using CSlickRun.UI.Controls;
using CSlickRun.UI.Windows;
using Newtonsoft.Json;

namespace CSlickRun.Logic;

/// <summary>
///     Klasse zu den Commands
/// </summary>
public class Command
{
    /// <summary>
    ///     Konstuktor
    /// </summary>
    /// <param name="name">
    ///     <see cref="Name" />
    /// </param>
    /// <param name="paths">
    ///     <see cref="Paths" />
    /// </param>
    /// <param name="note">
    ///     <see cref="Note" />
    /// </param>
    /// <param name="defaultCommand"> Kennzeichen, ob der Command ein DefaultCommand ist</param>
    public Command(string name, List<CommandPath>? paths, string? note, bool defaultCommand = false)
    {
        Name = name;
        Paths = paths;
        Note = note;
        DefaultCommand = defaultCommand;
    }

    /// <summary>
    ///     Konstruktor
    /// </summary>
    public Command()
    {
    }

    [JsonIgnore] public ItemStatus ItemStatus { get; set; }

    /// <summary>
    ///     Name des Befehls
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Commands, die ausgeführt werden sollen
    /// </summary>
    public List<CommandPath>? Paths { get; set; }

    /// <summary>
    ///     Notiz zum Befehl
    /// </summary>
    public string? Note { get; set; }

    public bool DefaultCommand { get; set; }

    /// <summary>
    ///     Führt den Befehl aus
    /// </summary>
    public void Execute()
    {
        if (CheckAndExecuteDefaultCommands() || Paths == null) return;
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
            if (!info.UseShellExecute) info.WorkingDirectory = path.StartupPath;
            process.StartInfo = info;
            process.Start();
        }
    }

    /// <summary>
    ///     Überprüft, ob es sich um einen Standardbefehl handelt und führt diesen aus
    /// </summary>
    /// <returns>true, falls es ein Standardcommand war, ansonsten false</returns>
    private bool CheckAndExecuteDefaultCommands()
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
                    return true;
                }

                var configWindow = new ConfigWindow();
                configWindow.Show();
                Keyboard.ClearFocus();
                Keyboard.Focus(configWindow);
                configWindow.Focus();
                return true;
            }
            case "Exit":
                Application.Current.Shutdown();
                return true;
            default:
                return false;
        }
    }
}