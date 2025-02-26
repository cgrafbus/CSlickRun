using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CSlickRun.UI.ViewModels.Base;
using CSlickRun.UI.Views;

namespace CSlickRun.UI.ViewModels
{
    public class ConfigWindowVm : ViewModelBase
    {
        private UserControl _currentView;
        public UserControl CurrentView
        {
            get => _currentView;
            set => SetField(ref _currentView, value);
        }

        public RelayCommand NavigationCommand { get; set; }

        public ConfigWindowVm()
        {
            NavigationCommand = new RelayCommand(ExecuteNavigation);
            CurrentView = new CommandView();
        }

        private void ExecuteNavigation (object? obj)
        {
            if (obj is Func<UserControl> viewFactory)
            {
                CurrentView = viewFactory();
            }
        }
    }
}
