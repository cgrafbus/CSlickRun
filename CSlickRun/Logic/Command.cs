using System.Diagnostics;
using System.Windows;
using CSlickRun.UI.Windows;

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
    public Command(string name, List<CommandPath>? paths, string? note)
    {
        Name = name;
        Paths = paths;
        Note = note;
    }


    /// <summary>
    ///     Konstruktor
    /// </summary>
    public Command()
    {
    }

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

    /// <summary>
    ///     Führt den Befehl aus
    /// </summary>
    public void Execute()
    {
        if (CheckAndExecuteDefaultCommands() || Paths == null)
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
    ///     Überprüft, ob es sich um einen Standardbefehl handelt und führt diesen aus
    /// </summary>
    /// <returns>true, falls es ein Standardcommand war, ansonsten false</returns>
    private bool CheckAndExecuteDefaultCommands()
    {
        if (Name == "Config")
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
            return true;
        }

        return false;
    }
}