using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CSlickRun.Logic;

namespace CSlickRun.UI.Controls;

/// <summary>
/// Interaction logic for NavigationBarButton.xaml
/// </summary>
public partial class NavigationBarButton : UserControl
{
    /// <summary>
    /// DependencyProperty for the group of the button.
    /// </summary>
    public static readonly DependencyProperty GroupProperty = DependencyProperty.Register(
        nameof(Group),
        typeof(string),
        typeof(NavigationBarButton));

    /// <summary>
    /// DependencyProperty for the command parameter.
    /// </summary>
    public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
        nameof(CommandParameter),
        typeof(Func<UserControl>),
        typeof(NavigationBarButton));

    /// <summary>
    /// DependencyProperty for the command to be executed.
    /// </summary>
    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
        nameof(Command),
        typeof(ICommand),
        typeof(NavigationBarButton));

    /// <summary>
    /// DependencyProperty for the text content of the button.
    /// </summary>
    public static readonly DependencyProperty TextContentProperty = DependencyProperty.Register(
        nameof(TextContent),
        typeof(string),
        typeof(NavigationBarButton));

    /// <summary>
    /// DependencyProperty for the active state of the button.
    /// </summary>
    public static readonly DependencyProperty ActiveProperty = DependencyProperty.Register(
        nameof(Active),
        typeof(bool),
        typeof(NavigationBarButton));

    /// <summary>
    /// DependencyProperty for the active color of the button.
    /// </summary>
    public static readonly DependencyProperty ActiveColorProperty = DependencyProperty.Register(
        nameof(ActiveColor),
        typeof(SolidColorBrush),
        typeof(NavigationBarButton));

    /// <summary>
    /// DependencyProperty for the background color of the button.
    /// </summary>
    public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register(
        nameof(BackgroundColor),
        typeof(SolidColorBrush),
        typeof(NavigationBarButton));

    /// <summary>
    /// Constructor
    /// </summary>
    public NavigationBarButton()
    {
        InitializeComponent();
        DataContext = this;
    }

    /// <summary>
    /// Gets or sets the text content of the button.
    /// </summary>
    public string TextContent
    {
        get => (string)GetValue(TextContentProperty);
        set => SetValue(TextContentProperty, value);
    }

    /// <summary>
    /// Gets or sets the group of the button.
    /// </summary>
    public string Group
    {
        get => (string)GetValue(GroupProperty);
        set => SetValue(GroupProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the button is active.
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
    /// Gets or sets the active color of the button.
    /// </summary>
    public SolidColorBrush ActiveColor
    {
        get => (SolidColorBrush)GetValue(ActiveColorProperty);
        set => SetValue(ActiveColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the background color of the button.
    /// </summary>
    public SolidColorBrush BackgroundColor
    {
        get => (SolidColorBrush)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the command to be executed when the button is clicked.
    /// </summary>
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    /// Gets or sets the command parameter.
    /// </summary>
    public Func<UserControl> CommandParameter
    {
        get => (Func<UserControl>)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    /// <summary>
    /// Event handler for the MouseLeftButtonDown event.
    /// </summary>
    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        if (Active)
        {
            return;
        }

        var windowParent = UIHelper.FindParent<Window>(this);
        List<NavigationBarButton> allNavBarButtonsInGroup;

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

}

