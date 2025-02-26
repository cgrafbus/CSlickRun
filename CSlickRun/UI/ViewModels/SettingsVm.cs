using System.Drawing;
using System.Windows;
using CSlickRun.Logic;
using CSlickRun.UI.ViewModels.Base;
using CSlickRun.UI.Windows;

namespace CSlickRun.UI.ViewModels;

public class SettingsVm : SettingsVm_Base
{
    

    public SettingsVm()
    {
        SaveCommand = new(ExecuteSaveAsync);
        Load();
    }

    private void Load()
    {
        Mapper.MapClasses(Global.GlobalSettings, this);
        var shortcut = Global.GlobalSettings.ShortCutCodes;
        if (shortcut == null || shortcut.Count == 0)
        {
            return;
        }
        if (shortcut.Contains(VirtualKeyCodes.ALTKEY))
        {
            AltBool = true;
        }
        if (shortcut.Contains(VirtualKeyCodes.WINKEY))
        {
            WinBool = true;
        }
        if (shortcut.Contains(VirtualKeyCodes.SHIFTKEY))
        {
            ShiftBool = true;
        }
        if (shortcut.Contains(VirtualKeyCodes.CTRLKEY))
        {
            StrgBool = true;
        }
        Shortcut = shortcut.Last();
    }

    

    private async Task ExecuteSaveAsync(object? arg)
    {
        if (!CheckIfShortCutValid())
        {
            MessageBox.Show($"Shortcut {Shortcut} not valid, save failed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (!CheckIfFieldsValid())
        {
            MessageBox.Show($"Fields not valid, save failed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        Mapper.MapClasses(this, Global.GlobalSettings);
        var codeList = BuildShortCodeList();
        Global.GlobalSettings.ShortCutCodes = codeList;

        await Global.GlobalSettings.SaveAsync();
        ((CommandLineVm)(((CommandLineWindow)(Application.Current.MainWindow)).DataContext)).InitUI();
    }

    public List<string> BuildShortCodeList()
    {
        List<string> codeList = new();
        if  (string.IsNullOrEmpty(Shortcut))
        {
            return new List<string>();
        }
        if (AltBool)
        {
            codeList.Add(VirtualKeyCodes.ALTKEY);
        }
        if (StrgBool)
        {
            codeList.Add(VirtualKeyCodes.CTRLKEY);
        }
        if (ShiftBool)
        {
            codeList.Add(VirtualKeyCodes.SHIFTKEY);
        }

        if (WinBool)
        {
            codeList.Add(VirtualKeyCodes.WINKEY);
        }
        codeList.Add(Shortcut.ToLower());

        return codeList;
    }

    private bool CheckIfShortCutValid()
    {
        return Shortcut != null && VirtualKeyCodes.KeyCodes.Keys.Contains(Shortcut);
    }

    private bool CheckIfFieldsValid()
    {
        return !string.IsNullOrEmpty(CommandLineBackgroundColor) && 
               !string.IsNullOrEmpty(CommandLineForegroundColor) && 
               !string.IsNullOrEmpty(CaretColor) && 
               !string.IsNullOrEmpty(AutoCompleteForegroundColor) && 
               !string.IsNullOrEmpty(CommandLineWidth) && 
               !string.IsNullOrEmpty(CommandLineHeight);
    }
}