using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CSlickRun.Logic;
using CSlickRun.UI.Views;

namespace CSlickRun.UI.ViewModels;

/// <summary>
/// Überliegendes ViewModel zu den Commands
/// </summary>
public partial class CommandVm : CommandVm_Base
{
    /// <summary>
    /// Konstruktor
    /// </summary>
    public CommandVm()
    {
        Commands = new ObservableCollection<Command>(Global.GlobalCommandManager.GetCommands() ?? []);

        // Übersicht der Commands anzeigen
        CurrentCommandView = new CommandListView(this);
    }

    /// <summary>
    /// Zeigt die Editieransicht für einen Befehl an
    /// </summary>
    /// <exception cref="AggregateException"></exception>
    [RelayCommand]
    private void Edit(object? obj)
    {
        CurrentCommandView = new EditCommandView((Command)obj! ?? throw new AggregateException(), this);
    }

    /// <summary>
    /// Zeigt die Editieransicht für einen neuen Befehl an
    /// </summary>
    [RelayCommand]
    private void Add(object? obj)
    {
        CurrentCommandView = new EditCommandView(new Command(), this);
    }

    /// <summary>
    /// Löscht einen Befehl
    /// </summary>
    /// <exception cref="AggregateException"></exception>
    [RelayCommand]
    private void Delete(object? obj)
    {
        Commands?.Remove((Command)obj! ?? throw new ArgumentNullException());
    }

    /// <summary>
    /// Speichert die Befehle
    /// </summary>
    [RelayCommand]
    private async Task Save(object? obj)
    {
        Global.GlobalCommandManager.SetCommands(Commands ?? []);

        await Global.GlobalCommandManager.SaveCommands();
    }
}