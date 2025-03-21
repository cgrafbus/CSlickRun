using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CSlickRun.Logic.Converter;

/// <summary>
/// Converts Colors to SolidColorbrush and other way around
/// </summary>
public class ColorToSolidColorBrushConverter : IValueConverter
{
    /// <inheritdoc cref="IValueConverter.Convert"/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value != null ? new SolidColorBrush((Color)value) : new SolidColorBrush();
    }

    /// <inheritdoc cref="IValueConverter.ConvertBack"/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value != null)
        {
            return ((SolidColorBrush)value).Color;
        }
        return new Color();
    }
}