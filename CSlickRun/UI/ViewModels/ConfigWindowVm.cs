using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CSlickRun.UI.ViewModels.Base;
using CSlickRun.UI.Views;

namespace CSlickRun.UI.ViewModels;

/// <summary>
/// ConfigWindow-ViewModel
/// </summary>
public partial class ConfigWindowVm : ViewModelBase
{
    [ObservableProperty] private UserControl currentView;

    /// <summary>
    /// Konstruktor
    /// </summary>
    public ConfigWindowVm()
    {
        CurrentView = new CommandView();
    }

    [RelayCommand]
    private void Navigation(object? obj)
    {
        if (obj is Func<UserControl> viewFactory)
        {
            CurrentView = viewFactory();
            CurrentView.Focus();
        }
    }
}