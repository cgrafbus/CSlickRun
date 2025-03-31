using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CSlickRun.Logic;
using CSlickRun.UI.Controls;
using CSlickRun.UI.Views;

namespace CSlickRun.UI.ViewModels;

/// <summary>
/// Command-ViewModel
/// </summary>
public partial class CommandVm : CommandVm_Base
{
    /// <summary>
    /// Constructor
    /// </summary>
    public CommandVm()
    {
        Commands = new ObservableCollection<Command>(Global.GlobalCommandManager.GetUserCommands());
        VisibleCommands = Commands;
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TestColumnWidth))]
    private double testWidth;


    public double TestColumnWidth => TestWidth / 3;
    /// <summary>
    /// Decreases index by one
    /// </summary>
    [RelayCommand]
    private void MoveIndexUp()
    {
        if (CurrentIndex > 0)
        {
            CurrentIndex -= 1;
        }
    }

    /// <summary>
    /// Increases index by one
    /// </summary>
    [RelayCommand]
    private void MoveIndexDown()
    {
        if (CurrentIndex < Commands.Count)
        {
            CurrentIndex += 1;
        }
    }

    [RelayCommand]
    private void Test(object? obj)
    {
        Debug.WriteLine($"Grid Width = {TestWidth}");
        Debug.WriteLine($"Col Width = {TestColumnWidth}");
    }


    /// <summary>
    /// Deletes a command
    /// </summary>
    /// <exception cref="ArgumentNullException">Throws when given object is not a Command</exception>
    [RelayCommand]
    private void Delete(object? obj)
    {
        if (obj is List<Command> commands)
        {
            foreach (var command in commands)
            {
                if (command.ItemStatus != ItemStatus.Deleted)
                {
                    command.OldItemStatus = command.ItemStatus;
                    command.ItemStatus = ItemStatus.Deleted;
                }
                else
                {
                    command.ItemStatus = command.OldItemStatus;
                }
                var index = Commands.IndexOf(command);
                Commands.RemoveAt(index);
                Commands.Insert(index, command);
            }
        }
        else if (obj is Command command)
        {
            if (command.ItemStatus != ItemStatus.Deleted)
            {
                command.OldItemStatus = command.ItemStatus;
                command.ItemStatus = ItemStatus.Deleted;
            }
            else
            {
                command.ItemStatus = command.OldItemStatus;
            }
            var index = Commands.IndexOf(command);
            Commands.RemoveAt(index);
            Commands.Insert(index, command);
        }
        else
        {
            throw new InvalidOperationException($"Object {obj} is not a command");
        }
        OnPropertyChanged(nameof(Commands));
    }

   

    /// <summary>
    /// Saves the commands
    /// </summary>
    [RelayCommand]
    private async Task Save(object? obj)
    {
        var commandList = Commands.ToList();
        foreach (var command in commandList.ToList())
        {
            if (command.ItemStatus == ItemStatus.Deleted)
            {
                commandList.Remove(command);
            }
            else
            {
                command.ItemStatus = ItemStatus.None;
            }
        }

        Global.GlobalCommandManager.SetUserCommands(commandList ?? []);
        Commands = new ObservableCollection<Command>(Global.GlobalCommandManager.GetUserCommands());
        await Global.GlobalCommandManager.SaveCommands();
        OnPropertyChanged(nameof(Commands));
    }

    [RelayCommand]
    private void UpdateEditSection(object? obj)
    {
        if (obj is Command command)
        {
            CurrentCommand = command;
            CommandPaths = new ObservableCollection<CommandPath>(command.Paths ?? []);
        }
        else
        {
            CurrentCommand = new Command();
            CommandPaths = [];
        }
    }

    [RelayCommand]
    private async Task Export()
    {
        if (SelectedCommands?.Any() == true)
        {
            await CommandFactory.ExportCommands(SelectedCommands.ToList());
        }
    }

    [RelayCommand]
    private async Task Import()
    {
        var importedCommands = await CommandFactory.ImportCommands();
        if (importedCommands != null)
        {
            foreach (var command in importedCommands.OfType<Command>())
            {
                command.ItemStatus = ItemStatus.New;
                Commands.Add(command);
            }
            OnPropertyChanged(nameof(Commands));
        }
    }
}