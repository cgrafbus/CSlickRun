using System.Globalization;
using System.Windows.Data;

namespace CSlickRun.Logic.Converter;

public class MultiBooleanConverter : IMultiValueConverter
{
    public object Convert(object[]? values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values == null || values.Length < 2)
            return false;

        var result = true;
        foreach (var value in values)
        {
            if (value is bool boolValue)
            {
                result &= boolValue;
            }
            else
            {
                return false;
            }
        }

        return result;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}