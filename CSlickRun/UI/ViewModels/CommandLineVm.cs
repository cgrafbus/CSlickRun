using System.Windows;
using CSlickRun.Logic;

namespace CSlickRun.UI.ViewModels;

/// <summary>
/// Commandline-ViewModel
/// </summary>
public class CommandLineVm : CommandLineVm_Base
{
    /// <summary>
    /// Constructor
    /// </summary>
    public CommandLineVm()
    {
        InitUI();
    }

    /// <summary>
    /// Applies the UI
    /// </summary>
    public void InitUI()
    {
        Mapper.MapClasses(Global.GlobalSettings, this);
        Left = 0;
        Top = SystemParameters.WorkArea.Height - Convert.ToInt32(CommandLineHeight);
        CurrentBackgroundColor = CommandLineInactiveBackgroundColor;
        CurrentBorderColor = BorderInactiveColor;
    }
}