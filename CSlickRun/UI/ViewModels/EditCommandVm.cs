using System.Collections.ObjectModel;
using System.Windows;
using CSlickRun.Logic;
using CSlickRun.UI.ViewModels.Base;
using CSlickRun.UI.Views;

namespace CSlickRun.UI.ViewModels;

public class EditCommandVm : ViewModelBase
{
    private readonly CommandVm _parentVm;

    private ObservableCollection<CommandPath>? _commandPaths;

    private CommandPath _selectedPath;

    public EditCommandVm()
    {
        CommandPaths = new ObservableCollection<CommandPath>
        {
            new()
            {
                Argument = "testArg",
                Path = "test",
                StartupPath = "testPathstart"
            },
            new()
            {
                Argument = "testArg2",
                Path = "test2",
                StartupPath = "testPathstart2"
            },
            new()
            {
                Argument = "testArg3",
                Path = "test3",
                StartupPath = "testPathstart3"
            }
        };
    }

    public EditCommandVm(CommandVm parentVm, Command command)
    {
        _parentVm = parentVm;
        CurrentCommand = command;
        CommandPaths = new ObservableCollection<CommandPath>();

        if (command.Paths != null)
        {
            foreach (var path in command.Paths)
            {
                CommandPaths.Add(path);
            }
        }

        SaveCommand = new RelayCommand(ExecuteSaveCommand);

        GoBackCommand = new RelayCommand(_ => _parentVm.CurrentCommandView = new CommandListView(_parentVm));
        DeletePathCommand = new RelayCommand(_ => CommandPaths?.Remove(SelectedPath));
    }

    public ObservableCollection<CommandPath>? CommandPaths
    {
        get => _commandPaths;
        set => SetField(ref _commandPaths, value);
    }

    public CommandPath SelectedPath
    {
        get => _selectedPath;
        set => SetField(ref _selectedPath, value);
    }

    public Command CurrentCommand { get; set; }
    public RelayCommand DeletePathCommand { get; }
    public RelayCommand SaveCommand { get; }
    public RelayCommand GoBackCommand { get; }

    private void ExecuteSaveCommand(object? obj)
    {
        if (string.IsNullOrWhiteSpace(CurrentCommand.Name) || string.IsNullOrEmpty(CurrentCommand.Name))
        {
            MessageBox.Show("Invalid Name");
            return;
        }

        CurrentCommand.Paths = CommandPaths?.ToList() ?? [];
        if (!_parentVm.Commands.Contains(CurrentCommand))
        {
            _parentVm.Commands.Add(CurrentCommand);
        }

        _parentVm.CurrentCommandView = new CommandListView(_parentVm);
        _parentVm.SaveCommand.Execute(this);
    }
}