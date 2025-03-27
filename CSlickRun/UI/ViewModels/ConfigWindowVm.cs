using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(CurrentViewIsListView))]
    private UserControl? currentView;

    

    public Visibility CurrentViewIsListView =>
        CurrentView?.DataContext is CommandVm
            ? Visibility.Visible
            : Visibility.Hidden;

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

    [RelayCommand]
    private async Task ExportCommands()
    {
        if (CurrentView?.DataContext is not CommandVm vm)
        {
            return;
        }
        switch (vm.CurrentCommandView?.DataContext)
        {
            case CommandListVm listVm:
                await listVm.ExportCommand.ExecuteAsync(null);
                return;
        }
    }

    [RelayCommand]
    private async Task ImportCommands()
    {
        if (CurrentView?.DataContext is not CommandVm { CurrentCommandView.DataContext:CommandListVm listVm })
        {
            return;
        }
        await listVm.ImportCommand.ExecuteAsync(null);
    }
}