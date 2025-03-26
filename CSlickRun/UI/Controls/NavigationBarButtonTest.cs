using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CSlickRun.UI.Controls
{
    public class NavigationBarButtonTest : Button
    {
        /// <summary>
        /// DependencyProperty for the group of the button.
        /// </summary>
        public static readonly DependencyProperty GroupProperty = DependencyProperty.Register(
            nameof(Group),
            typeof(string),
            typeof(NavigationBarButtonTest));

        /// <summary>
        /// DependencyProperty for the active state of the button.
        /// </summary>
        public static readonly DependencyProperty ActiveProperty = DependencyProperty.Register(
            nameof(Active),
            typeof(bool),
            typeof(NavigationBarButtonTest));

        /// <summary>
        /// DependencyProperty for the active color of the button.
        /// </summary>
        public static readonly DependencyProperty ActiveColorProperty = DependencyProperty.Register(
            nameof(ActiveColor),
            typeof(SolidColorBrush),
            typeof(NavigationBarButtonTest));

        public static readonly DependencyProperty InactiveColorProperty = DependencyProperty.Register(
            nameof(InactiveColor), typeof(SolidColorBrush), typeof(NavigationBarButtonTest));


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
                Background = value ? ActiveColor : InactiveColor;
                UpdateLayout();
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
        /// Gets or sets the active color of the button.
        /// </summary>
        public SolidColorBrush InactiveColor
        {
            get => (SolidColorBrush)GetValue(InactiveColorProperty);
            set => SetValue(InactiveColorProperty, value);
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
            List<NavigationBarButtonTest> allNavBarButtonsInGroup;

            if (windowParent == null)
            {
                var userControlParent = UIHelper.FindParent<UserControl>(this);
                allNavBarButtonsInGroup = UIHelper.FindAllChildrenWithType<NavigationBarButtonTest>(userControlParent)
                    .Where(navButton => navButton?.Group == Group).ToList();
            }
            else
            {
                allNavBarButtonsInGroup = UIHelper.FindAllChildrenWithType<NavigationBarButtonTest>(windowParent)
                    .Where(navButton => navButton?.Group == Group).ToList();
            }

            foreach (var but in allNavBarButtonsInGroup.OfType<NavigationBarButtonTest>())
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
}
