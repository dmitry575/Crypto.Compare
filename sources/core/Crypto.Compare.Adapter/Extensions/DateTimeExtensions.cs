namespace Crypto.Compare.Adapter.Extensions;

public static class DateTimeExtensions
{
    private static readonly DateTime ZeroDate = new DateTime(1970, 1, 1, 0, 0, 0);

    public static long GetTimestamp(this DateTime dateTime)
        => (long)dateTime.Subtract(ZeroDate).TotalMilliseconds;

    public static long GetTimestamp(this DateTime? dateTime)
        => dateTime == null ? 0 : (long)dateTime.Value.Subtract(ZeroDate).TotalMilliseconds;

    public static DateTime GetDateTime(this long timestamp)
        => ZeroDate.AddMilliseconds(timestamp).ToUniversalTime();

    public static DateTime GetDateTime(this long? timestamp)
        => timestamp == null ? ZeroDate : ZeroDate.AddMilliseconds(timestamp.Value).ToUniversalTime();
}
