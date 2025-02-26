using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CSlickRun.UI;

public static class UIHelper
{
    /// <summary>
    ///     Findet den nächsten Parent des Typen T
    /// </summary>
    /// <typeparam name="T">Typ des Parents</typeparam>
    /// <param name="child">Item, dessen Parent gesucht wird</param>
    /// <returns>Den Parent</returns>
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

    public static void UpdateCommandLineUI()
    {

    }

    public static String ConvertColorToHex(System.Drawing.Color c)
        => $"#{c.R:X2}{c.G:X2}{c.B:X2}";

    public static SolidColorBrush ConvertHexToBrush(string hex)
    {
        BrushConverter converter = new BrushConverter();
        return (SolidColorBrush)converter.ConvertFromString(hex) ?? new SolidColorBrush();
    }


    public static String ConvertColorToRgb(System.Drawing.Color c)
        => $"RGB({c.R},{c.G},{c.B})";

    /// <summary>
    ///     TODO
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static List<UIElement?> FindDirectChildren(DependencyObject? parent)
    {
        return FindAllChildrenWithType<UIElement>(parent);
    }

    /// <summary>
    ///     TODO
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static List<T?> FindAllChildrenWithType<T>(DependencyObject? parent)
    {
        var children = new List<T?>();

        if (parent == null) return children;
        // Überprüfe alle direkten Children
        for (var i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);

            if (child is T tElement) children.Add(tElement);

            children.AddRange(FindAllChildrenWithType<T>(child));
        }

        return children;
    }

    public static BitmapImage GetImageFromPath(string? path)
    {
        if (path == null) return new BitmapImage();
        var image = new BitmapImage();
        image.BeginInit();
        image.UriSource = new Uri(path);
        image.EndInit();
        return image;
    }

    public static BitmapImage GetImageFromBytes(byte[] imagedata)
    {
        using (var ms = new System.IO.MemoryStream(imagedata))
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad; // here
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }
}