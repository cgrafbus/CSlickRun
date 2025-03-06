using System.Collections.ObjectModel;
using System.Windows.Controls;
using CSlickRun.Logic;
using CSlickRun.UI.ViewModels.Base;

namespace CSlickRun.UI.ViewModels;

/// <summary>
/// Base-Klasse zum überliegenden Command-ViewModel
/// </summary>
public class CommandVm_Base : ViewModelBase
{
    /// <summary>
    /// Alle Commands
    /// </summary>
    private ObservableCollection<Command>? _commands;

    /// <summary>
    /// Momentanes ViewModel
    /// </summary>
    private UserControl? _currentCommandView;

    /// <summary>
    /// <see cref="_currentCommandView"/>
    /// </summary>
    public UserControl? CurrentCommandView
    {
        get => _currentCommandView;
        set => SetField(ref _currentCommandView, value);
    }

    /// <summary>
    /// <see cref="_commands"/>
    /// </summary>
    public ObservableCollection<Command>? Commands
    {
        get => _commands;
        set => SetField(ref _commands, value);
    }

    public required RelayCommand EditCommand { get; set; }
    public required RelayCommand AddCommand { get; set; }
    public required RelayCommand DeleteCommand { get; set; }
    public required AsyncRelayCommand SaveCommand { get; set; }
}