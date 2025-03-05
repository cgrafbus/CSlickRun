using System.Collections.ObjectModel;
using CSlickRun.Logic;
using CSlickRun.UI.ViewModels.Base;
using CSlickRun.UI.Views;

namespace CSlickRun.UI.ViewModels;

/// <summary>
/// Überliegendes ViewModel zu den Commands
/// </summary>
public class CommandVm : CommandVm_Base
{
    /// <summary>
    /// Konstruktor
    /// </summary>
    public CommandVm()
    {
        Commands = new ObservableCollection<Command>(Global.GlobalCommandManager.UserCommands ?? []);

        // Übersicht der Commands anzeigen
        CurrentCommandView = new CommandListView(this);

        EditCommand = new RelayCommand(ExecuteEditCommand);
        AddCommand = new RelayCommand(ExecuteAddCommand);
        DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
        SaveCommand = new AsyncRelayCommand(ExecuteSaveCommandAsync);
    }

    /// <summary>
    /// Zeigt die Editieransicht für einen Befehl an
    /// </summary>
    /// <exception cref="AggregateException"></exception>
    private void ExecuteEditCommand(object? obj)
    {
        CurrentCommandView = new EditCommandView((Command)obj! ?? throw new AggregateException(), this);
    }

    /// <summary>
    /// Zeigt die Editieransicht für einen neuen Befehl an
    /// </summary>
    private void ExecuteAddCommand(object? obj)
    {
        CurrentCommandView = new EditCommandView(new Command(), this);
    }

    /// <summary>
    /// Löscht einen Befehl
    /// </summary>
    /// <exception cref="AggregateException"></exception>
    private void ExecuteDeleteCommand(object? obj)
    {
        Commands?.Remove((Command)obj! ?? throw new AggregateException());
    }

    /// <summary>
    /// Speichert die Befehle
    /// </summary>
    private async Task ExecuteSaveCommandAsync(object? obj)
    {
        Global.GlobalCommandManager.UserCommands?.Clear();
        Global.GlobalCommandManager.UserCommands?.AddRange(Commands ?? new ObservableCollection<Command>());

        await Global.GlobalCommandManager.SaveCommands();
    }
}