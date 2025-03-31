using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CSlickRun.Logic;
using CSlickRun.UI.Controls;
using CSlickRun.UI.Windows;

namespace CSlickRun.UI;

/// <summary>
/// Provides helper methods for UI operations.
/// </summary>
public static class UIHelper
{
    /// <summary>
    /// Finds the nearest parent of the specified type T.
    /// </summary>
    /// <typeparam name="T">The type of the parent.</typeparam>
    /// <param name="child">The item whose parent is to be found.</param>
    /// <returns>The parent of the specified type, or null if not found.</returns>
    public static T? FindParent<T>(DependencyObject child) where T : DependencyObject
    {
        while (true)
        {
            var parentObject = VisualTreeHelper.GetParent(child);

            switch (parentObject)
            {
                case null:
                    return null;
                case T parent:
                    return parent;
                default:
                    child = parentObject;
                    break;
            }
        }
    }

    /// <summary>
    /// Converts a hex string to a SolidColorBrush.
    /// </summary>
    /// <param name="hex">The hex string.</param>
    /// <returns>The resulting SolidColorBrush.</returns>
    public static SolidColorBrush ConvertHexToBrush(string? hex)
    {
        var converter = new BrushConverter();
        return (SolidColorBrush?)converter.ConvertFromString(hex ?? string.Empty) ?? new SolidColorBrush();
    }

    /// <summary>
    /// Gets all children of the specified type T from the specified control.
    /// </summary>
    /// <typeparam name="T">The type of the children.</typeparam>
    /// <param name="parent">The parent control.</param>
    /// <returns>A list of children of the specified type.</returns>
    public static List<T> FindAllChildrenWithType<T>(DependencyObject? parent)
    {
        var children = new List<T>();

        if (parent == null) return children;
        // Check all direct children
        for (var i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);

            if (child is T tElement) children.Add(tElement);

            children.AddRange(FindAllChildrenWithType<T>(child));
        }

        return children;
    }

    /// <summary>
    /// Gets all children of the specified control.
    /// </summary>
    /// <param name="parent">The parent control.</param>
    /// <returns>A list of all children.</returns>
    public static List<UIElement> FindAllChildren(DependencyObject? parent)
    {
        return FindAllChildrenWithType<UIElement>(parent ?? null);
    }

    /// <summary>
    /// Gets an image from the specified path.
    /// </summary>
    /// <param name="path">The path to the image.</param>
    /// <returns>The resulting BitmapImage.</returns>
    public static BitmapImage GetImageFromPath(string? path)
    {
        try
        {
            if (path == null) return new BitmapImage();
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(path, UriKind.Relative);
            image.EndInit();
            return image;
        }
        catch
        {
            return new BitmapImage();
        }
    }

    public static async Task ShowToastMessage(string message, ToastMessageType type)
    {
        var confWindow = Application.Current.Windows
            .OfType<ConfigWindow>()
            .FirstOrDefault();

        if (confWindow != null)
        {
            confWindow.Activate();
            await confWindow.MainToastMessage.Show(message, type);
        }
    }

    public static bool ChangeControlContentsByKeyPressBehavior(KeyPressBehavior behavior, DependencyObject? source)
    {
        try
        {
            if (source == null)
            {
                return false;
            }

            var behaviourCommand = behavior.Command;
            var behaviourKey = behavior.Key.ToString();

            foreach (var ctrl in FindAllChildrenWithType<Button>(source).Where(ctrl => ctrl.Command == behaviourCommand)
                         .Where(ctrl => ctrl.Content.ToString()?.Contains($" ({behaviourKey})") == false))
            {
                ctrl.Content += $" ({behaviourKey})";
            }

            return true;
        }
        catch
        {
            return false;
        }
    }
}