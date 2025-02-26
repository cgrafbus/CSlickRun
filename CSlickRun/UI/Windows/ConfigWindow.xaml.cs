using CSlickRun.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CSlickRun.UI.Views;

namespace CSlickRun.UI.Windows
{
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
    }
}
