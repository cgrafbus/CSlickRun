using System.Windows;
using System.Windows.Controls;

namespace CSlickRun.UI.Controls;

/// <summary>
/// Interaction logic for PanelHeader.xaml
/// </summary>
public partial class PanelHeader
{
    /// <summary>
    /// DependencyProperty for the content of the header label.
    /// </summary>
    public static readonly DependencyProperty HeaderLabelContentProperty = DependencyProperty.Register(
        nameof(HeaderLabelContent),
        typeof(string),
        typeof(PanelHeader));

    /// <summary>
    /// Constructor
    /// </summary>
    public PanelHeader()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    /// <summary>
    /// Gets or sets the content of the header label.
    /// </summary>
    public string HeaderLabelContent
    {
        get => (string)GetValue(HeaderLabelContentProperty);
        set => SetValue(HeaderLabelContentProperty, value);
    }

    /// <summary>
    /// Handles the Loaded event.
    /// </summary>
    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        HeaderLabel.Content = HeaderLabelContent;
    }
}