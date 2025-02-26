using System.Collections.ObjectModel;
using CSlickRun.Logic;
using CSlickRun.UI.ViewModels.Base;
using CSlickRun.UI.Views;

namespace CSlickRun.UI.ViewModels;

public class CommandVm : CommandVm_Base
{
    public CommandVm()
    {
        Commands = new ObservableCollection<Command>(Global.GlobalCommandManager.UserCommands ?? []);

        // Übersicht zuerst anzeigen
        CurrentCommandView = new CommandListView(this);

        EditCommand = new RelayCommand(ExecuteEditCommand);
        AddCommand = new RelayCommand(ExecuteAddCommand);
        DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
        SaveCommand = new AsyncRelayCommand(ExecuteSaveCommandAsync);
    }

    private void ExecuteEditCommand(object? obj)
    {
        CurrentCommandView = new EditCommandView((Command)obj! ?? throw new AggregateException(), this);
    }

    private void ExecuteAddCommand(object? obj)
    {
        CurrentCommandView = new EditCommandView(new Command(), this);
    }

    private void ExecuteDeleteCommand(object? obj)
    {
        Commands.Remove((Command)obj! ?? throw new AggregateException());
    }

    private async Task ExecuteSaveCommandAsync(object? obj)
    {
        Global.GlobalCommandManager.UserCommands?.Clear();
        Global.GlobalCommandManager.UserCommands?.AddRange(Commands ?? new ObservableCollection<Command>());

        await Global.GlobalCommandManager.SaveCommands();
    }
}