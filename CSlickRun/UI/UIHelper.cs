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

    /// <summary>
    /// Konvertiert einen Hex-String in eine SolidColorBrush
    /// </summary>
    /// <param name="hex">Hex-String</param>
    /// <returns>Ergebener SolidColorBrush</returns>
    public static SolidColorBrush ConvertHexToBrush(string? hex)
    {
        var converter = new BrushConverter();
        return (SolidColorBrush?)converter.ConvertFromString(hex ?? string.Empty) ?? new SolidColorBrush();
    }

    /// <summary>
    ///     Gibt alle direkten Children der übergebenen Control zurück
    /// </summary>
    /// <param name="parent">Parent-Control</param>
    /// <returns>Liste der Children</returns>
    public static List<UIElement?> FindDirectChildren(DependencyObject? parent)
    {
        return FindAllChildrenWithType<UIElement>(parent);
    }

    /// <summary>
    ///     Gibt alle Children des übergebenen Typen T der übergebenen Control zurück
    /// </summary>
    /// <param name="parent">Parent-Control</param>
    /// <typeparam name="T">Typ der Children</typeparam>
    /// <returns>Liste der Children</returns>
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

    /// <summary>
    ///    Gibt ein Bild aus einem Pfad zurück
    /// </summary>
    /// <param name="path">Pfad</param>
    /// <returns>Bild</returns>
    public static BitmapImage GetImageFromPath(string? path)
    {
        if (path == null) return new BitmapImage();
        var image = new BitmapImage();
        image.BeginInit();
        image.UriSource = new Uri(path);
        image.EndInit();
        return image;
    }

    /// <summary>
    ///    Gibt ein Bild aus einem Byte-Array zurück
    /// </summary>
    /// <param name="imagedata">Image-Daten</param>
    /// <returns>Bild</returns>
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