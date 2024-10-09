using System.Globalization;
using GymBookingManager.Core.Utils;

namespace GymBookingManager.Maui.Client.Converters;

public class FriendlyDateTimeConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is not DateTime dateTime
            ? value
            : dateTime.ToFriendlyDateTimeString();
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}