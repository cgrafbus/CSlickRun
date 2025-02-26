using System.Windows;
using CSlickRun.Logic;
using CSlickRun.UI.ViewModels.Base;

namespace CSlickRun.UI.ViewModels;

public class CommandLineVm : CommandLineVm_Base
{
    public CommandLineVm()
    {
        InitUI();
    }

    public void InitUI()
    {
        var screenHeight = SystemParameters.WorkArea.Height;
        CommandLineBackgroundColor = Global.GlobalSettings.CommandLineBackgroundColor;
        CommandLineForegroundColor = Global.GlobalSettings.CommandLineForegroundColor;
        CaretColor = Global.GlobalSettings.CaretColor;
        AutoCompleteForegroundColor = Global.GlobalSettings.AutoCompleteForegroundColor;
        AutoCompleteBackgroundColor = Global.GlobalSettings.AutoCompleteBackgroundColor;
        BorderColor = Global.GlobalSettings.BorderColor;
        CommandLineHeight = Global.GlobalSettings.CommandLineHeight;
        CommandLineWidth = Global.GlobalSettings.CommandLineWidth;
        AlwaysOnTop = Global.GlobalSettings.AlwaysOnTop;
        Left = 0;
        Top = screenHeight - Convert.ToInt32(CommandLineHeight);
    }
}