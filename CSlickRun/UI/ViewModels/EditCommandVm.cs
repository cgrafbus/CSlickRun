using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CSlickRun.Logic;
using CSlickRun.UI.Controls;
using CSlickRun.UI.ViewModels.Base;
using CSlickRun.UI.Views;

namespace CSlickRun.UI.ViewModels;

/// <summary>
/// ViewModel for editing commands.
/// </summary>
public partial class EditCommandVm : ViewModelBase
{
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(ForbidShortcutExecution))]
    private bool anyTextBoxFocused;

    [ObservableProperty] private ObservableCollection<CommandPath>? commandPaths;

    [ObservableProperty] private Command currentCommand;

    [ObservableProperty] private CommandVm parentVm;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(ForbidShortcutExecution))]
    private bool pathGridFocused;

    [ObservableProperty] private CommandPath? selectedPath;

    /// <summary>
    /// Initializes a new instance of the <see cref="EditCommandVm"/> class.
    /// </summary>
    /// <param name="parentvm">The parent ViewModel.</param>
    /// <param name="command">The command to be edited.</param>
    /// <param name="editCommandVm">This instance.</param>
    public EditCommandVm(CommandVm parentvm, Command command, out EditCommandVm editCommandVm)
    {
        editCommandVm = this;
        ParentVm = parentvm;
        CurrentCommand = command;
        CommandPaths = new ObservableCollection<CommandPath>();

        CurrentCommand.ItemStatus =
            string.IsNullOrEmpty(CurrentCommand.Name) || string.IsNullOrWhiteSpace(CurrentCommand.Name) ||
            CurrentCommand.ItemStatus == ItemStatus.New
                ? ItemStatus.New
                : ItemStatus.None;

        if (command.Paths != null)
        {
            foreach (var path in command.Paths)
                CommandPaths.Add(path);
        }
    }

    /// <summary>
    /// Gets a value indicating whether shortcut execution is forbidden.
    /// </summary>
    public bool ForbidShortcutExecution => PathGridFocused || AnyTextBoxFocused;

    /// <summary>
    /// Saves the command.
    /// </summary>
    [RelayCommand]
    private void Save(object? obj)
    {
        if (!UeberpruefeName(out var fehler))
        {
            MessageBox.Show(fehler, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        CurrentCommand.Paths = CommandPaths?
            .Where(path => !string.IsNullOrEmpty(path.Path)
                           && !string.IsNullOrWhiteSpace(path.Path))
            .ToList() ?? new List<CommandPath>();

        if (ParentVm.Commands.Contains(CurrentCommand) && CurrentCommand.ItemStatus != ItemStatus.New)
        {
            CurrentCommand.ItemStatus = ItemStatus.Modified;
        }
        else if (!ParentVm.Commands.Contains(CurrentCommand))
        {
            ParentVm.Commands.Add(CurrentCommand);
        }

        //ParentVm.CurrentCommandView = new CommandListView(ParentVm);
    }

    /// <summary>
    /// Checks if the command name is valid.
    /// </summary>
    /// <param name="fehler">The error message, if any.</param>
    /// <returns>True if the name is valid, otherwise false.</returns>
    private bool UeberpruefeName(out string? fehler)
    {
        fehler = null;
        if (!string.IsNullOrWhiteSpace(CurrentCommand.Name) && !string.IsNullOrEmpty(CurrentCommand.Name))
        {
            return true;
        }

        fehler = "Name cannot be empty";
        return false;
    }

    /// <summary>
    /// Deletes the selected path.
    /// </summary>
    [RelayCommand]
    private void DeletePath(object? obj)
    {
        CommandPaths?.Remove(SelectedPath!);
    }

    /// <summary>
    /// Navigates back to the command list view.
    /// </summary>
    [RelayCommand]
    private void GoBack(object? obj)
    {
       // ParentVm.CurrentCommandView = new CommandListView(ParentVm);
    }

    /// <summary>
    /// Tests the command.
    /// </summary>
    [RelayCommand]
    private void TestCommand(object? obj)
    {
        var testCommand = new Command
        {
            Paths = CommandPaths?
                .Where(path => !string.IsNullOrEmpty(path.Path)
                               && !string.IsNullOrWhiteSpace(path.Path))
                .ToList() ?? new List<CommandPath>()
        };
        try
        {
            testCommand.Execute();
        }
        catch
        {
            MessageBox.Show("Couldn't execute command", "Test failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}