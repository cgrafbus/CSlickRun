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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CSlickRun.Logic;
using CSlickRun.UI.ViewModels;

namespace CSlickRun.UI.Views
{
    /// <summary>
    /// Interaction logic for EditCommandView.xaml
    /// </summary>
    public partial class EditCommandView : UserControl
    {
        public EditCommandView(Command command, CommandVm parentVm)
        {
            InitializeComponent();
            DataContext = new EditCommandVm(parentVm, command);
        }
    }
}
