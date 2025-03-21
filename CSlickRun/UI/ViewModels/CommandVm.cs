using System.Collections.ObjectModel;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.Input;
using CSlickRun.Logic;
using CSlickRun.UI.Controls;
using CSlickRun.UI.Views;

namespace CSlickRun.UI.ViewModels;

/// <summary>
///     Überliegendes ViewModel zu den Commands
/// </summary>
public partial class CommandVm : CommandVm_Base
{
    /// <summary>
    ///     Konstruktor
    /// </summary>
    public CommandVm()
    {
        Commands = new ObservableCollection<Command>(Global.GlobalCommandManager.GetUserCommands());
        CurrentCommandView = new CommandListView(this);
    }
    /// <summary>
    ///     Zeigt die Editieransicht für einen Befehl an
    /// </summary>
    /// <exception cref="AggregateException"></exception>
    [RelayCommand]
    private void Edit(object? obj)
    {
        CurrentCommandView = new EditCommandView((Command?)obj ?? new Command(), this);
    }

    /// <summary>
    ///     Zeigt die Editieransicht für einen neuen Befehl an
    /// </summary>
    [RelayCommand]
    private void Add(object? obj)
    {
        CurrentCommandView = new EditCommandView(new Command(), this);
    }

    /// <summary>
    ///     Löscht einen Befehl
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    [RelayCommand]
    private void Delete(object? obj)
    {
        if (obj is not Command command)
        {
            throw new ArgumentNullException($"Parameter {obj} is not a valid command.");
        }

        command.ItemStatus = ItemStatus.Deleted;
        var index = Commands.IndexOf(command);
        Commands.RemoveAt(index);
        Commands.Insert(index, command);
        OnPropertyChanged(nameof(Commands));
    }

    /// <summary>
    ///     Speichert die Befehle
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