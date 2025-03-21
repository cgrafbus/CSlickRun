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
/// ViewModel for managing application settings.
/// </summary>
public partial class SettingsVm : SettingsVm_Base
{
    private const string AppName = "CSlickRun";
    private static readonly string StartupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

    /// <summary>
    /// Constructor
    /// </summary>
    public SettingsVm()
    {
        Load();
    }

    /// <summary>
    /// Maps the global settings to the ViewModel.
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
    /// Saves the settings.
    /// </summary>
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
    /// Sets or removes the auto-start entry in the Windows startup folder.
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
    /// Creates a shortcut in the startup folder.
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
    /// Removes the shortcut from the startup folder.
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
    /// Builds the shortcut code list.
    /// </summary>
    /// <returns>The constructed shortcut code list.</returns>
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
    /// Checks if the shortcut is valid.
    /// </summary>
    /// <returns>True if valid, otherwise false.</returns>
    private bool CheckIfShortCutValid()
    {
        return Shortcut != null && VirtualKeyCodes.KeyCodes.Keys.Contains(Shortcut);
    }

    /// <summary>
    /// Checks if all important fields are valid.
    /// </summary>
    /// <returns>True if valid, otherwise false.</returns>
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


