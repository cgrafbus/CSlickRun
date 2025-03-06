using System.IO;
using System.Reflection;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using CSlickRun.Logic;
using CSlickRun.UI.Windows;
using IWshRuntimeLibrary;
using File = System.IO.File;

namespace CSlickRun.UI.ViewModels;

/// <summary>
/// Settings-ViewModel
/// </summary>
public partial class SettingsVm : SettingsVm_Base
{
    private const string AppName = "CSlickRun";
    private static readonly string StartupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

    /// <summary>
    /// Kontruktor
    /// </summary>
    public SettingsVm()
    {
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

    [RelayCommand]
    private async Task Save()
    {
        if (!CheckIfShortCutValid())
        {
            MessageBox.Show($"Shortcut {Shortcut} not valid, save failed.", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return;
        }

        if (!CheckIfFieldsValid())
        {
            MessageBox.Show("Fields not valid, save failed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        Mapper.MapClasses(this, Global.GlobalSettings);
        var codeList = BuildShortCodeList();
        Global.GlobalSettings.ShortCutCodes = codeList;

        await Global.GlobalSettings.SaveAsync();
        ((CommandLineVm)(((CommandLineWindow)(Application.Current.MainWindow!)).DataContext)).InitUI();
        Global.ResetGlobalHotkey();
        ApplyAutoStartup();
    }


    /// <summary>
    /// Setzt oder entfernt den Autostart-Eintrag im Startup-Folder von Windows
    /// </summary>
    private void ApplyAutoStartup()
    {
        if (AutoStartup)
        {
            CreateShortcutInStartupFolder();
        }
        else
        {
            RemoveShortcutFromStartupFolder();
        }
    }

    /// <summary>
    /// Erstellt eine Verknüpfung im Startup-Ordner.
    /// </summary>
    private void CreateShortcutInStartupFolder()
    {
        var shortcutPath = Path.Combine(StartupFolderPath, $"{AppName}.lnk");
        var exeName = Assembly.GetExecutingAssembly().GetName().Name + ".exe";
        var targetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, exeName);

        var shell = new WshShell();
        var shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
        shortcut.IconLocation = Global.IconFile;
        shortcut.Description = "Launch CSlickRun";
        shortcut.TargetPath = targetPath;
        shortcut.Save();
    }

    /// <summary>
    /// Entfernt die Verknüpfung aus dem Startup-Ordner.
    /// </summary>
    private void RemoveShortcutFromStartupFolder()
    {
        var shortcutPath = Path.Combine(StartupFolderPath, $"{AppName}.lnk");
        if (File.Exists(shortcutPath))
        {
            File.Delete(shortcutPath);
        }
    }

    /// <summary>
    /// Baut den ShortcutCode zusammen
    /// </summary>
    /// <returns>Zusammengebauter ShortcutCode</returns>
    private List<string> BuildShortCodeList()
    {
        List<string> codeList = new();
        if (string.IsNullOrEmpty(Shortcut))
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
               !string.IsNullOrEmpty(CommandLineInactiveBackgroundColor) &&
               !string.IsNullOrEmpty(SelectionColor) &&
               !string.IsNullOrEmpty(BorderInactiveColor) &&
               !string.IsNullOrEmpty(CommandLineWidth) &&
               !string.IsNullOrEmpty(CommandLineHeight);
    }
}