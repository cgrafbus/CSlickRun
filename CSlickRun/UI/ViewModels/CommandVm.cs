using System.Collections.ObjectModel;
using System.Windows.Controls;
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
        CurrentCommandView = new CommandListView(this);
    }

    /// <summary>
    /// Sets current view to edit a command
    /// </summary>
    [RelayCommand]
    private void Edit(object? obj)
    {
        CurrentCommandView = new EditCommandView((Command?)obj ?? new Command(), this);
    }

    /// <summary>
    /// Sets current view to add a new commnad
    /// </summary>
    [RelayCommand]
    private void Add(object? obj)
    {
        CurrentCommandView = new EditCommandView(new Command(), this);
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
        CurrentCommandView = new CommandListView(this);
    }
}