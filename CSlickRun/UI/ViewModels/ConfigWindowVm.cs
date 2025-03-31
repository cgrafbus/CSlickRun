using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CSlickRun.UI.ViewModels.Base;
using CSlickRun.UI.Views;
using ViewBase = CSlickRun.UI.Views.ViewBase;

namespace CSlickRun.UI.ViewModels;

/// <summary>
/// ConfigWindow-ViewModel
/// </summary>
public partial class ConfigWindowVm : ViewModelBase
{
    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(CurrentViewIsListView))]
    private ViewBase? currentView;

    [ObservableProperty]
    private GridLength firstColLength;

    [ObservableProperty]
    private Visibility sidePanelVisibility;

    [ObservableProperty]
    private double viewHostWidth;

    [RelayCommand]
    private void UpdateLayout()
    {
        if (FirstColLength == new GridLength(0))
        {
            FirstColLength = new (1, GridUnitType.Star);
            SidePanelVisibility = Visibility.Visible;
            ViewHostWidth = 1440;
        }
        else
        {
            FirstColLength = new(0);
            SidePanelVisibility = Visibility.Collapsed;
            ViewHostWidth = 1563;
        }
        if (CurrentView is ISubView view)
        {
            view.OnLayoutChanged();
        }
    }

    

    public Visibility CurrentViewIsListView =>
        CurrentView?.DataContext is CommandVm
            ? Visibility.Visible
            : Visibility.Hidden;

    /// <summary>
    /// Constructor
    /// </summary>
    public ConfigWindowVm()
    {
        FirstColLength = new(1, GridUnitType.Star);
        SidePanelVisibility = Visibility.Visible;
        ViewHostWidth = 1440;
    }

    /// <summary>
    /// Sets Current View
    /// </summary>
    /// <param name="obj">Function that creates view</param>
    [RelayCommand]
    private void Navigation(object? obj)
    {
        if (obj is not Func<ViewBase> viewFactory)
        {
            return;
        }
        if (CurrentView is ISubView oldView)
        {
            if (!oldView.OnExit())
            {
                return;
            }
        }
        CurrentView = viewFactory();
        Keyboard.ClearFocus();
        Keyboard.Focus(CurrentView);
        CurrentView.Focus();
        if (CurrentView is ISubView newView)
        {
            newView.OnEnter();
        }
    }

    [RelayCommand]
    private async Task ExportCommands()
    {
        if (CurrentView?.DataContext is not CommandVm vm)
        {
            return;
        }
       // switch (vm.CurrentCommandView?.DataContext)
        //{
          //  case CommandListVm listVm:
          //      await listVm.ExportCommand.ExecuteAsync(null);
           //     return;
        //}
    }

    [RelayCommand]
    private async Task ImportCommands()
    {
        /*if (CurrentView?.DataContext is not CommandVm { CurrentCommandView.DataContext:CommandListVm listVm })
        {
            return;
        }
        await listVm.ImportCommand.ExecuteAsync(null);*/
    }
}