using System.Windows.Controls;
using CSlickRun.UI.ViewModels.Base;
using CSlickRun.UI.Views;

namespace CSlickRun.UI.ViewModels;

/// <summary>
/// ConfigWindow-ViewModel
/// </summary>
public class ConfigWindowVm : ViewModelBase
{
    private UserControl _currentView;

    /// <summary>
    /// Konstruktor
    /// </summary>
    public ConfigWindowVm()
    {
        NavigationCommand = new RelayCommand(ExecuteNavigation);
        _currentView = new CommandView();
    }

    /// <summary>
    /// Momentane View
    /// </summary>
    public UserControl CurrentView
    {
        get => _currentView;
        set => SetField(ref _currentView, value);
    }

    public RelayCommand NavigationCommand { get; set; }

    /// <summary>
    /// Navigiert zwischen den Views
    /// </summary>
    private void ExecuteNavigation(object? obj)
    {
        if (obj is Func<UserControl> viewFactory) CurrentView = viewFactory();
    }
}