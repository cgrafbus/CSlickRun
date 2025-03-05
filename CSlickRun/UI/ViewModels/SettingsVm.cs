using System.Windows;
using CSlickRun.Logic;
using CSlickRun.UI.Windows;

namespace CSlickRun.UI.ViewModels;

/// <summary>
/// Settings-ViewModel
/// </summary>
public class SettingsVm : SettingsVm_Base
{
    /// <summary>
    /// Kontruktor
    /// </summary>
    public SettingsVm()
    {
        SaveCommand = new(ExecuteSaveAsync);
        Load();
    }

    /// <summary>
    /// Mappt die Globalen Settings in das ViewModel
    /// </summary>
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

    

    /// <summary>
    /// Speichert die Settings, aktualisiert die UI und ggf. den globalen Hotkey
    /// </summary>
    /// <param name="arg"></param>
    /// <returns></returns>
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
        Global.ResetGlobalHotkey();
    }

    /// <summary>
    /// Baut den ShortcutCode zusammen
    /// </summary>
    /// <returns>Zusammengebauter ShortcutCode</returns>
    private List<string> BuildShortCodeList()
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

    /// <summary>
    /// Prüft, ob der ShortCut valide ist
    /// </summary>
    /// <returns>true, falls valide, ansonsten false</returns>
    private bool CheckIfShortCutValid()
    {
        return Shortcut != null && VirtualKeyCodes.KeyCodes.Keys.Contains(Shortcut);
    }

    /// <summary>
    /// Prüft, ob alle wichtigen Felder valide sind
    /// </summary>
    /// <returns>true, falls valide, ansonsten false</returns>
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