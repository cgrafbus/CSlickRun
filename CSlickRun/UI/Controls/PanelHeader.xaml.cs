using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace CSlickRun.UI.Controls
{
    /// <summary>
    /// Interaction logic for PanelHeader.xaml
    /// </summary>
    public partial class PanelHeader : UserControl
    {
        public PanelHeader()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            HeaderLabel.Content = HeaderLabelContent;
        }

        public string HeaderLabelContent
        {
            get => (string)GetValue(HeaderLabelContentProperty);
            set => SetValue(HeaderLabelContentProperty, value);
        }

        public static readonly DependencyProperty HeaderLabelContentProperty = DependencyProperty.Register(
            nameof(HeaderLabelContent),
            typeof(string),
            typeof(PanelHeader));
    }
}
