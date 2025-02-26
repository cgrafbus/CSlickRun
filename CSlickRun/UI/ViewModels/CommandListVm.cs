using CSlickRun.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CSlickRun.UI.ViewModels.Base;

namespace CSlickRun.UI.ViewModels
{
    public class  CommandListVm : ViewModelBase
    {
        private readonly CommandVm _parentVm;
        private Command _selectedCommand;

        public ObservableCollection<Command> Commands => _parentVm.Commands ?? [];

        public Command? SelectedCommand
        {
            get => _selectedCommand;
            set => SetField(ref _selectedCommand, value);
        }

        public ICommand EditCommand => _parentVm.EditCommand;
        public ICommand DeleteCommand => _parentVm.DeleteCommand;
        public ICommand AddCommand => _parentVm.AddCommand;
        public ICommand SaveCommand => _parentVm.SaveCommand;


        public CommandListVm(CommandVm parentVm)
        {
            _parentVm = parentVm;
            SelectedCommand = Commands.FirstOrDefault();
        }
    }
}
