using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CSlickRun.UI.Controls;

namespace CSlickRun.UI.Views;

/// <summary>
/// Interaction logic for SettingsView.xaml
/// </summary>
public partial class SettingsView
{
    /// <summary>
    /// Constructor
    /// </summary>
    public SettingsView()
    {
        InitializeComponent();
        LoadSideBar();
    }

    /// <summary>
    /// Loads the sidebar with labels corresponding to headers in the content host.
    /// </summary>
    private void LoadSideBar()
    {
        var headers = UIHelper.FindAllChildrenWithType<PanelHeader>(ContentHost);
        foreach (var header in headers)
        {
            var label = new Label
            {
                Content = header?.HeaderLabelContent,
                Style = Application.Current.FindResource("ClickableLabel") as Style,
                Height = 50
            };
            label.MouseLeftButtonDown += LabelOnMouseLeftButtonDown;
            label.Foreground = new SolidColorBrush(Colors.Aqua);
            Panel.SetZIndex(label, 100);
            SideBarHost.Children.Add(label);
        }

        UpdateLayout();
    }

    /// <summary>
    /// Event handler for the MouseLeftButtonDown event on labels.
    /// Scrolls to the corresponding header in the content host.
    /// </summary>
    private void LabelOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is not Label label) return;
        var header = UIHelper.FindAllChildrenWithType<PanelHeader>(ContentHost)
            .First(header => header?.HeaderLabelContent == (string?)label.Content);
        ScrollToElement(header);
    }

    /// <summary>
    /// Scrolls to the specified element within the scroller.
    /// </summary>
    private void ScrollToElement(UIElement element)
    {
        var transform = element.TransformToVisual(Scroller.Content as Visual ?? throw new InvalidOperationException());
        var position = transform.Transform(new Point(0, 0));
        Scroller.ScrollToVerticalOffset(position.Y);
    }

    /// <summary>
    /// Event handler for the PreviewTextInput event on UI elements.
    /// Ensures that only numeric input is allowed.
    /// </summary>
    private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !int.TryParse(e.Text, out _);
    }
}

