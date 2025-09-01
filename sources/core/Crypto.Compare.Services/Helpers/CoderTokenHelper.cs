using System.Security.Cryptography;
using System.Text;

namespace Crypto.Compare.Services.Helpers;

/// <summary>
///     Code and decode authorization header
/// </summary>
public static class CoderTokenHelper
{
    /// <summary>
    ///     Crypt data for token
    /// </summary>
    /// <param name="data">Data in string</param>
    /// <param name="keyLength">Lenght of times hexs</param>
    public static byte[] Crypt(string data, byte keyLength)
    {
        var dataBytes = Encoding.UTF8.GetBytes(data);
        var md5 = HashHelpers.ToMd5(dataBytes);
        var result = new byte[dataBytes.Length + 2];
        var i = keyLength;

        // in last symbol = keyLength
        result[^1] = keyLength;
        var key = md5[0];

        while (i > 0)
        {
            result[0] = key;
            for (var k = 0; k < dataBytes.Length; k++) result[1 + k] = (byte)(dataBytes[k] ^ key);

            i--;
            key = result[1];
        }

        return result;
    }

    /// <summary>
    ///     Decrypt data
    /// </summary>
    /// <param name="hexData">Crypted data</param>
    public static string DeCrypt(string hexData)
    {
        var dataBytes = Convert.FromHexString(hexData);

        return DeCrypt(dataBytes);
    }

    /// <summary>
    ///     Decrypt data
    /// </summary>
    /// <param name="dataBytes">Crypted data</param>
    public static string DeCrypt(byte[] dataBytes)
    {
        var key = dataBytes[0];
        var i = dataBytes[^1];
        var data = new byte[dataBytes.Length - 2];
        Array.Copy(dataBytes, 1, data, 0, data.Length);

        while (i > 0)
        {
            for (var k = 0; k < data.Length; k++) data[k] = (byte)(dataBytes[k + 1] ^ key);

            i--;
            key = data[1];
        }

        return Encoding.UTF8.GetString(data);
    }

    /// <summary>
    ///     Get key for auth 2FA or 3FA
    /// </summary>
    /// <param name="data">String with some data</param>
    public static string GetKeyAuthFa(string data)
    {
#pragma warning disable CA1311
        var hash = HashHelpers.ToSha1(Encoding.UTF8.GetBytes(data));
        return Convert.ToHexString(hash).ToLower();
#pragma warning restore CA1311
    }

    /// <summary>
    ///     Get key for changes (recovery for example) (old name - sk)
    /// </summary>
    /// <param name="data">String with some data</param>
    public static string GetSk(string data)
    {
#pragma warning disable CA1311
        var hash = HashHelpers.ToSha256(Encoding.UTF8.GetBytes(data));
        return Convert.ToHexString(hash).ToLower();
#pragma warning restore CA1311
    }

    /// <summary>
    ///     Get hash with sign
    /// </summary>
    /// <param name="data">Data to hash</param>
    /// <param name="key">Salt hashing</param>
    public static string GetHashToken(string data, string key)
    {
        var buffer = Encoding.UTF8.GetBytes(data);
        var hash = new HMACSHA256(Encoding.UTF8.GetBytes(key));

        var hashBytes = hash.ComputeHash(buffer);

#pragma warning disable CA1311
        return Convert.ToHexString(hashBytes)
            .ToLower();
#pragma warning restore CA1311
    }


    public static byte[] GetByteFromInt(int intValue)
    {
        var intBytes = BitConverter.GetBytes(intValue);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(intBytes);
        return intBytes;
    }
}