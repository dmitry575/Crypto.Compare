using System.Security.Cryptography;

namespace Crypto.Compare.Services.Helpers;

public class HashHelpers
{
    /// <summary>
    /// Convert to MD5 hash
    /// </summary>
    public static byte[] ToMd5(byte[] data)
    {
        if (data.Length == 0)
        {
            return Array.Empty<byte>();
        }

#pragma warning disable CA5351
        using var md5 = MD5.Create();
#pragma warning restore CA5351
#pragma warning disable CA1850
        return md5.ComputeHash(data);
#pragma warning restore CA1850
    }

    /// <summary>
    /// Convert data to Sha1
    /// </summary>
    public static byte[] ToSha1(byte[] data)
    {
        if (data.Length == 0)
        {
            return Array.Empty<byte>();
        }

#pragma warning disable CA5350
        using var sha1 = SHA1.Create();
#pragma warning restore CA5350
#pragma warning disable CA1850
        return sha1.ComputeHash(data);
#pragma warning restore CA1850 
    }

    /// <summary>
    /// Convert data to Sha256
    /// </summary>
    public static byte[] ToSha256(byte[] data)
    {
        if (data.Length == 0)
        {
            return Array.Empty<byte>();
        }

        using var sha256 = SHA256.Create();

#pragma warning disable CA1850 
        return sha256.ComputeHash(data);
#pragma warning restore CA1850
    }
}