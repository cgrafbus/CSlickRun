using System.Collections.ObjectModel;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CSlickRun.Logic;
using CSlickRun.UI.ViewModels.Base;
using CSlickRun.UI.Views;

namespace CSlickRun.UI.ViewModels;

public class CommandVm : ViewModelBase
{
    private UserControl _currentCommandView;
    public UserControl CurrentCommandView
    {
        get => _currentCommandView;
        set => SetField(ref _currentCommandView, value);
    }

    private ObservableCollection<Command>? _commands;

    public ObservableCollection<Command>? Commands
    {
        get => _commands;
        set => SetField(ref _commands, value);
    }
    public ICommand EditCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand ShowCommandsList { get; }
    public ICommand SaveCommand { get; }


    public CommandVm()
    {
        Commands = new ObservableCollection<Command>(Global.GlobalCommandManager.UserCommands ?? new());

        // Übersicht zuerst anzeigen
        CurrentCommandView = new CommandListView(this);

        EditCommand = new RelayCommand(command => CurrentCommandView = new EditCommandView((Command)command, this));
        AddCommand = new RelayCommand(_ => CurrentCommandView = new EditCommandView(new Command(), this));
        ShowCommandsList = new RelayCommand(_ => CurrentCommandView = new CommandListView(this));
        DeleteCommand = new RelayCommand(command => Commands.Remove((Command)command));
        SaveCommand = new AsyncRelayCommand(ExecuteSaveCommand);
    }

    private async Task ExecuteSaveCommand(object? obj)
    {
        Global.GlobalCommandManager.UserCommands.Clear();
        Global.GlobalCommandManager.UserCommands.AddRange(Commands ?? new());

        await Global.GlobalCommandManager.SaveCommands();
    }
}