using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CSlickRun.Logic;

namespace CSlickRun.UI.ViewModels
{
    public class CommandListVm_Base
    {
        private readonly CommandVm _parentVm;
        public ObservableCollection<Command> Commands => _parentVm.Commands ?? [];

        public ICommand EditCommand => _parentVm.EditCommand;

    }
}
