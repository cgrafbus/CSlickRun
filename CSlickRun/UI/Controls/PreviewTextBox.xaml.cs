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

namespace CSlickRun.UI.Controls
{
    /// <summary>
    /// Interaction logic for PreviewTextBox.xaml
    /// </summary>
    public partial class PreviewTextBox : UserControl
    {
        public static readonly DependencyProperty PreviewProperty = DependencyProperty.Register(
            nameof(Preview), typeof(string), typeof(PreviewTextBox), new PropertyMetadata(""));

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text), typeof(string), typeof(PreviewTextBox), new PropertyMetadata(""));

        public string Preview
        {
            get => (string)GetValue(PreviewProperty);
            set => SetValue(PreviewProperty, value);
        }
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public PreviewTextBox()
        {
            InitializeComponent();
        }
    }
}
