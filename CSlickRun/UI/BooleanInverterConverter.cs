using System.Globalization;
using System.Windows.Data;

namespace CSlickRun.UI;

/// <summary>
/// Converts a bool to the negative
/// </summary>
public class BooleanInverterConverter : IValueConverter
{
    /// <inheritdoc cref="IValueConverter.Convert"/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return !boolValue;
        }
        return false;
    }

    /// <inheritdoc cref="IValueConverter.ConvertBack"/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return !boolValue;
        }
        return false;
    }
}