namespace CSlickRun.Logic;

/// <summary>
/// Command-Path class
/// </summary>
public class CommandPath
{
    /// <summary>
    /// Constructor, required for DataGrid
    /// </summary>
    public CommandPath()
    {
        Console.Write("");
    }

    /// <summary>
    /// Path of Command
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// Argument of Command
    /// </summary>
    public string? Argument { get; set; }

    /// <summary>
    /// Startup-Path of Command
    /// </summary>
    public string? StartupPath { get; set; }
}