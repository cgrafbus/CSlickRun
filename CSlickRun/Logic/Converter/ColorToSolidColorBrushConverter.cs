using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CSlickRun.Logic.Converter;

public class ColorToSolidColorBrushConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value != null)
            return new SolidColorBrush((Color)value);

        return value ?? new SolidColorBrush();
    }


    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value != null)
            return ((SolidColorBrush)value).Color;

        return value ?? new Color();
    }
}