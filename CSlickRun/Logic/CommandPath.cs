namespace CSlickRun.Logic;

/// <summary>
///    Klasse zu den Befehlspfaden
/// </summary>
public class CommandPath
{
    public CommandPath()
    {
        Console.Write("");
        // Durch DataGrid wird ein Parameterloser Konstruktor benötigt
    }

    /// <summary>
    /// Pfad des Befehls
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// Argument des Befehls
    /// </summary>
    public string Argument { get; set; }

    /// <summary>
    /// Startpfad des Befehls
    /// </summary>
    public string StartupPath { get; set; }
}