using System.Windows;
using System.Windows.Data;
using CSlickRun.UI.Controls;
using CSlickRun.UI.Views;

namespace CSlickRun.UI.Windows;

/// <summary>
/// Interaction logic for ConfigWindow.xaml
/// </summary>
public partial class ConfigWindow : Window
{
    public ConfigWindow()
    {
        InitializeComponent();
        InitNavbarLogic();
    }

    private void InitNavbarLogic()
    {
        CommandNavButton.CommandParameter = () => new CommandView();
        SettingsNavButton.CommandParameter = () => new SettingsView();

        foreach (var child in NavBar.Children)
        {
            if (child is not NavigationBarButton navbarButton)
            {
                continue;
            }

            var commandBinding = new Binding("NavigationCommand")
            {
                Source = DataContext
            };
            BindingOperations.SetBinding(navbarButton, NavigationBarButton.CommandProperty, commandBinding);
        }

        CommandNavButton.Active = true;
    }

    private void CloseWindowButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void MinimizeWindowButton_OnClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
}