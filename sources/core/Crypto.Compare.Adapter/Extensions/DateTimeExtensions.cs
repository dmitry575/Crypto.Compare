namespace Crypto.Compare.Adapter.Extensions;

public static class DateTimeExtensions
{
    private static readonly DateTime ZeroDate = new(1970, 1, 1, 0, 0, 0);

    public static long GetTimestamp(this DateTime dateTime)
    {
        return (long)dateTime.Subtract(ZeroDate).TotalMilliseconds;
    }

    public static long GetTimestamp(this DateTime? dateTime)
    {
        return dateTime == null ? 0 : (long)dateTime.Value.Subtract(ZeroDate).TotalMilliseconds;
    }

    public static DateTime GetDateTime(this long timestamp)
    {
        return ZeroDate.AddMilliseconds(timestamp).ToUniversalTime();
    }

    public static DateTime GetDateTime(this long? timestamp)
    {
        return timestamp == null ? ZeroDate : ZeroDate.AddMilliseconds(timestamp.Value).ToUniversalTime();
    }
}