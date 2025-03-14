using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using CSlickRun.Logic;
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

        // Übersicht der Commands anzeigen
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
        if (obj is not Command command) throw new ArgumentNullException($"Parameter {obj} is not a valid command.");

        if (MessageBox.Show($"Are you sure you want to delete the Command {command.Name}", "Confirmation",
                MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            return;
        Commands?.Remove(command);
    }

    /// <summary>
    ///     Speichert die Befehle
    /// </summary>
    [RelayCommand]
    private async Task Save(object? obj)
    {
        Global.GlobalCommandManager.SetUserCommands(Commands ?? []);

        await Global.GlobalCommandManager.SaveCommands();
    }
}