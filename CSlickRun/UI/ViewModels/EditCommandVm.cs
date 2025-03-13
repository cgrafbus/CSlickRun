using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CSlickRun.Logic;
using CSlickRun.UI.ViewModels.Base;
using CSlickRun.UI.Views;

namespace CSlickRun.UI.ViewModels;

/// <summary>
///     ViewModel für die Bearbeitung von Befehlen.
/// </summary>
public partial class EditCommandVm : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<CommandPath>? commandPaths;
    [ObservableProperty] private Command currentCommand;
    [ObservableProperty] private CommandVm parentVm;
    [ObservableProperty] private CommandPath? selectedPath;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ForbidShortcutExecution))]
    private bool pathGridFocused;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ForbidShortcutExecution))]
    private bool anyTextBoxFocused;

    public bool ForbidShortcutExecution => PathGridFocused || AnyTextBoxFocused;

    /// <summary>
    ///     Initialisiert eine neue Instanz der <see cref="EditCommandVm" /> Klasse.
    /// </summary>
    /// <param name="parentvm">Das übergeordnete ViewModel.</param>
    /// <param name="command">Der zu bearbeitende Befehl.</param>
    public EditCommandVm(CommandVm parentvm, Command command)
    {
        ParentVm = parentvm;
        CurrentCommand = command;
        CommandPaths = [];

        if (command.Paths != null)
            foreach (var path in command.Paths)
                CommandPaths.Add(path);
    }


    [RelayCommand]
    private void Save(object? obj)
    {
        if (string.IsNullOrWhiteSpace(CurrentCommand.Name) || string.IsNullOrEmpty(CurrentCommand.Name))
        {
            MessageBox.Show("Invalid Name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        CurrentCommand.Paths = CommandPaths?
            .Where(path => !string.IsNullOrEmpty(path.Path)
                           && !string.IsNullOrWhiteSpace(path.Path))
            .ToList() ?? [];

        if (ParentVm.Commands != null && !ParentVm.Commands.Contains(CurrentCommand))
            ParentVm.Commands.Add(CurrentCommand);

        ParentVm.CurrentCommandView = new CommandListView(ParentVm);
    }

    [RelayCommand]
    private void DeletePath(object? obj)
    {
        CommandPaths?.Remove(SelectedPath!);
    }

    [RelayCommand]
    private void GoBack(object? obj)
    {
        ParentVm.CurrentCommandView = new CommandListView(ParentVm);
    }

    [RelayCommand]
    private void TestCommand(object? obj)
    {
        var testCommand = new Command
        {
            Paths = CommandPaths?
                .Where(path => !string.IsNullOrEmpty(path.Path)
                               && !string.IsNullOrWhiteSpace(path.Path))
                .ToList() ?? []
        };
        testCommand.Execute();
    }
}