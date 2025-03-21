using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CSlickRun.UI.ViewModels.Base;

namespace CSlickRun.UI.ViewModels;

/// <summary>
/// ConfigWindow-ViewModel
/// </summary>
public partial class ConfigWindowVm : ViewModelBase
{
    [ObservableProperty] private UserControl currentView;

    /// <summary>
    /// Constructor
    /// </summary>
    public ConfigWindowVm()
    {
    }

    /// <summary>
    /// Sets Current View
    /// </summary>
    /// <param name="obj">Function that creates view</param>
    [RelayCommand]
    private void Navigation(object? obj)
    {
        if (obj is Func<UserControl> viewFactory)
        {
            CurrentView = viewFactory();
            Keyboard.ClearFocus();
            Keyboard.Focus(CurrentView);
            CurrentView.Focus();
        }
    }
}