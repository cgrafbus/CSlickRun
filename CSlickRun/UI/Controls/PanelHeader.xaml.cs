using System.Windows;
using System.Windows.Controls;

namespace CSlickRun.UI.Controls;

/// <summary>
///     Interaction logic for PanelHeader.xaml
/// </summary>
public partial class PanelHeader : UserControl
{
    public static readonly DependencyProperty HeaderLabelContentProperty = DependencyProperty.Register(
        nameof(HeaderLabelContent),
        typeof(string),
        typeof(PanelHeader));

    /// <summary>
    /// Konstruktor
    /// </summary>
    public PanelHeader()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    /// <summary>
    ///     Content des HeaderLabels
    /// </summary>
    public string HeaderLabelContent
    {
        get => (string)GetValue(HeaderLabelContentProperty);
        set => SetValue(HeaderLabelContentProperty, value);
    }

    /// <summary>
    ///     On-Loaded Event-Handler
    /// </summary>
    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        HeaderLabel.Content = HeaderLabelContent;
    }
}