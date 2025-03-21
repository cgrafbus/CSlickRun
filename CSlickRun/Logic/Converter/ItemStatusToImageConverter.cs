using System.Globalization;
using System.IO;
using System.Windows.Data;
using CSlickRun.Logic.Helpers;
using CSlickRun.UI;
using CSlickRun.UI.Controls;

namespace CSlickRun.Logic.Converter;

/// <summary>
/// Converts an ItemStatus to a BitmapImage
/// </summary>
public class ItemStatusToImageConverter : IValueConverter
{
    /// <inheritdoc cref="IValueConverter.Convert"/>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not ItemStatus status)
        {
            return null;
        }
        return UIHelper.GetImageFromPath(Path.Combine(Global.ImagePath,
            status.GetImagePathFromAttribute() ?? string.Empty));
    }

    /// <inheritdoc cref="IValueConverter.ConvertBack"/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}