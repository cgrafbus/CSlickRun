using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CSlickRun.UI.Controls;

/// <summary>
/// Interaction logic for NavigationBarButton.xaml
/// </summary>
public partial class NavigationBarButton : UserControl
{
    public static readonly DependencyProperty GroupProperty = DependencyProperty.Register(
        nameof(Group),
        typeof(string),
        typeof(NavigationBarButton));

    public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
        nameof(CommandParameter),
        typeof(Func<UserControl>),
        typeof(NavigationBarButton));

    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
        nameof(Command),
        typeof(ICommand),
        typeof(NavigationBarButton));

    public static readonly DependencyProperty TextContentProperty = DependencyProperty.Register(
        nameof(TextContent),
        typeof(string),
        typeof(NavigationBarButton));

    public static readonly DependencyProperty ActiveProperty = DependencyProperty.Register(
        nameof(Active),
        typeof(bool),
        typeof(NavigationBarButton));

    public static readonly DependencyProperty ActiveColorProperty = DependencyProperty.Register(
        nameof(ActiveColor),
        typeof(SolidColorBrush),
        typeof(NavigationBarButton));

    public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register(
        nameof(BackgroundColor),
        typeof(SolidColorBrush),
        typeof(NavigationBarButton));

    /// <summary>
    /// Konstruktor
    /// </summary>
    public NavigationBarButton()
    {
        InitializeComponent();
        DataContext = this;
    }

    /// <summary>
    /// Text des Buttons
    /// </summary>
    public string TextContent
    {
        get => (string)GetValue(TextContentProperty);
        set => SetValue(TextContentProperty, value);
    }

    /// <summary>
    /// Gruppe des Buttons
    /// </summary>
    public string Group
    {
        get => (string)GetValue(GroupProperty);
        set => SetValue(GroupProperty, value);
    }

    /// <summary>
    /// Gibt an, ob der Button aktiv ist.
    /// </summary>
    public bool Active
    {
        get => (bool)GetValue(ActiveProperty);
        set
        {
            SetValue(ActiveProperty, value);
            Background = value ? ActiveColor : BackgroundColor;
        }
    }

    /// <summary>
    /// Die Farbe des Buttons, wenn er aktiv ist.
    /// </summary>
    public SolidColorBrush ActiveColor
    {
        get => (SolidColorBrush)GetValue(ActiveColorProperty);
        set => SetValue(ActiveColorProperty, value);
    }

    /// <summary>
    /// Die Hintergrundfarbe des Buttons.
    /// </summary>
    public SolidColorBrush BackgroundColor
    {
        get => (SolidColorBrush)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    /// <summary>
    /// Der Befehl, der ausgeführt wird, wenn der Button geklickt wird.
    /// </summary>
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    /// Das Parameter des Befehls.
    /// </summary>
    public Func<UserControl> CommandParameter
    {
        get => (Func<UserControl>)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    /// <summary>
    /// Event-Handler für das MouseLeftButtonDown-Ereignis.
    /// </summary>
    /// <param name="e">Die Ereignis-Daten.</param>
    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        try
        {
            if (Active)
            {
                return;
            }

            var windowParent = UIHelper.FindParent<Window>(this);
            List<NavigationBarButton?> allNavBarButtonsInGroup;

            if (windowParent == null)
            {
                var userControlParent = UIHelper.FindParent<UserControl>(this);
                allNavBarButtonsInGroup = UIHelper.FindAllChildrenWithType<NavigationBarButton>(userControlParent)
                    .Where(navButton => navButton?.Group == Group).ToList();
            }
            else
            {
                allNavBarButtonsInGroup = UIHelper.FindAllChildrenWithType<NavigationBarButton>(windowParent)
                    .Where(navButton => navButton?.Group == Group).ToList();
            }

            foreach (var but in allNavBarButtonsInGroup.OfType<NavigationBarButton>())
            {
                if (but != this)
                {
                    but.Active = false;
                }
            }

            Active = true;
            Command.Execute(CommandParameter);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}

