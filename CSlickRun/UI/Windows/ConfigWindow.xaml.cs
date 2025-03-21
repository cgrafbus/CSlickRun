﻿using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using CSlickRun.UI.Controls;
using CSlickRun.UI.Views;

namespace CSlickRun.UI.Windows;

/// <summary>
/// Interaction logic for ConfigWindow.xaml
/// </summary>
public partial class ConfigWindow
{
    /// <summary>
    /// Constructor
    /// </summary>
    public ConfigWindow()
    {
        InitializeComponent();
        InitNavbarLogic();
    }

    /// <summary>
    /// Initializes the Navigation bar
    /// </summary>
    private void InitNavbarLogic()
    {
        CommandNavButton.CommandParameter = () => new CommandView();
        SettingsNavButton.CommandParameter = () => new SettingsView();
        HelpNavButton.CommandParameter = () => new HelpView();

        foreach (var child in NavBar.Children)
        {
            if (child is not NavigationBarButton navbarButton) continue;

            var commandBinding = new Binding("NavigationCommand")
            {
                Source = DataContext
            };
            BindingOperations.SetBinding(navbarButton, NavigationBarButton.CommandProperty, commandBinding);
        }

        var args = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left)
        {
            RoutedEvent = MouseLeftButtonDownEvent
        };
        CommandNavButton.RaiseEvent(args);
    }

    /// <summary>
    /// Closes the Window
    /// </summary>
    private void CloseWindowButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    /// <summary>
    /// Minimizes the Window
    /// </summary>
    private void MinimizeWindowButton_OnClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
}