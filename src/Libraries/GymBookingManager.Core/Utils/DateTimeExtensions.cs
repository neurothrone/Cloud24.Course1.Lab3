namespace GymBookingManager.Core.Utils;

public static class DateTimeExtensions
{
    public static string ToFriendlyDateTimeString(this DateTime dateTime)
    {
        var today = DateTime.Today;
        if (dateTime.Date == today)
        {
            return $"Today, {dateTime:HH:mm}";
        }

        var tomorrow = today.AddDays(1);
        if (dateTime.Date == tomorrow)
        {
            return $"Tomorrow, {dateTime:HH:mm}";
        }

        var yesterday = today.AddDays(-1);
        return dateTime.Date == yesterday
            ? $"Yesterday, {dateTime:HH:mm}"
            : $"{dateTime:d MMM HH:mm}";
    }

    public static DateTime TomorrowAt(int hour, int minute)
    {
        DateTime tomorrow = DateTime.Today.AddDays(1);
        return new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, hour, minute, 0);
    }

    public static DateTime TodayAt(int hour, int minute)
    {
        DateTime today = DateTime.Today;
        return new DateTime(today.Year, today.Month, today.Day, hour, minute, 0);
    }

    public static DateTime YesterdayAt(int hour, int minute)
    {
        DateTime yesterday = DateTime.Today.AddDays(-1);
        return new DateTime(yesterday.Year, yesterday.Month, yesterday.Day, hour, minute, 0);
    }
}