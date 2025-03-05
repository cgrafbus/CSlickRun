using System.Collections.ObjectModel;
using System.Windows;
using CSlickRun.Logic;
using CSlickRun.UI.ViewModels.Base;
using CSlickRun.UI.Views;

namespace CSlickRun.UI.ViewModels;

/// <summary>
/// ViewModel für die Bearbeitung von Befehlen.
/// </summary>
public class EditCommandVm : ViewModelBase
{
    private readonly CommandVm _parentVm;
    private ObservableCollection<CommandPath>? _commandPaths;
    private CommandPath? _selectedPath;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="EditCommandVm"/> Klasse.
    /// </summary>
    /// <param name="parentVm">Das übergeordnete ViewModel.</param>
    /// <param name="command">Der zu bearbeitende Befehl.</param>
    public EditCommandVm(CommandVm parentVm, Command command)
    {
        _parentVm = parentVm;
        CurrentCommand = command;
        CommandPaths = [];

        if (command.Paths != null)
        {
            foreach (var path in command.Paths)
            {
                CommandPaths.Add(path);
            }
        }

        SaveCommand = new RelayCommand(ExecuteSaveCommand);
        GoBackCommand = new RelayCommand(_ => _parentVm.CurrentCommandView = new CommandListView(_parentVm));
        DeletePathCommand = new RelayCommand(_ => CommandPaths?.Remove(SelectedPath!));
        TestCommandCommand = new RelayCommand(ExecuteTestCommand);
    }

    /// <summary>
    /// Der aktuelle Befehl.
    /// </summary>
    public Command CurrentCommand { get; set; }

    /// <summary>
    /// Die Sammlung der Befehlspfade.
    /// </summary>
    public ObservableCollection<CommandPath>? CommandPaths
    {
        get => _commandPaths;
        set => SetField(ref _commandPaths, value);
    }

    /// <summary>
    /// Der ausgewählte Befehlspfad.
    /// </summary>
    public CommandPath? SelectedPath
    {
        get => _selectedPath;
        set => SetField(ref _selectedPath, value);
    }

    /// <summary>
    /// Befehl zum Löschen eines Pfades.
    /// </summary>
    public RelayCommand DeletePathCommand { get; }

    /// <summary>
    /// Befehl zum Speichern des aktuellen Befehls.
    /// </summary>
    public RelayCommand SaveCommand { get; }

    /// <summary>
    /// Befehl zum Zurückkehren zur Befehlsliste.
    /// </summary>
    public RelayCommand GoBackCommand { get; }

    /// <summary>
    /// Befehl zum Testen des aktuellen Befehls.
    /// </summary>
    public RelayCommand TestCommandCommand { get; }

    /// <summary>
    /// Führt den Speichern-Befehl aus.
    /// </summary>
    /// <param name="obj">Das Befehlsparameter.</param>
    private void ExecuteSaveCommand(object? obj)
    {
        if (string.IsNullOrWhiteSpace(CurrentCommand.Name) || string.IsNullOrEmpty(CurrentCommand.Name))
        {
            MessageBox.Show("Invalid Name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        CurrentCommand.Paths = CommandPaths?
            .Where(path => !string.IsNullOrEmpty(path.Path)
                           && !string.IsNullOrWhiteSpace(path.Path))
            .ToList() ?? new List<CommandPath>();

        if (_parentVm.Commands != null && !_parentVm.Commands.Contains(CurrentCommand))
        {
            _parentVm.Commands.Add(CurrentCommand);
        }

        _parentVm.CurrentCommandView = new CommandListView(_parentVm);
        _parentVm.SaveCommand.Execute(this);
    }

    /// <summary>
    /// Führt den Test-Befehl aus.
    /// </summary>
    /// <param name="obj">Das Befehlsparameter.</param>
    private void ExecuteTestCommand(object? obj)
    {
        var testCommand = new Command
        {
            Paths = CommandPaths?
                .Where(path => !string.IsNullOrEmpty(path.Path)
                               && !string.IsNullOrWhiteSpace(path.Path))
                .ToList() ?? new List<CommandPath>()
        };
        testCommand.Execute();
    }
}
