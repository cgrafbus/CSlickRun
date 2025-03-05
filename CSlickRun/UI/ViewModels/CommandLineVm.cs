using System.Windows;
using CSlickRun.Logic;
using CSlickRun.UI.ViewModels.Base;

namespace CSlickRun.UI.ViewModels;

/// <summary>
/// ViewModel für die Kommandozeile.
/// </summary>
public class CommandLineVm : CommandLineVm_Base
{
    /// <summary>
    /// Konstrukor
    /// </summary>
    public CommandLineVm()
    {
        InitUI();
    }

    /// <summary>
    /// Initialisiert die UI
    /// </summary>
    public void InitUI()
    {
        Mapper.MapClasses(Global.GlobalSettings, this);
        Left = 0;
        Top = SystemParameters.WorkArea.Height - Convert.ToInt32(CommandLineHeight);
    }
}