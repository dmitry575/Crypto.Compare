using Crypto.Compare.Adapter.Config;

namespace Crypto.Compare.Mexc.Config;

/// <summary>
///     Configuration of Mexc Crypto Market
/// </summary>
public class MexcConfig : BaseConfig
{
    /// <summary>
    ///     Access key for market
    /// </summary>
    public string AccessKey { get; set; } = null!;

    /// <summary>
    ///     Secret key for signe request
    /// </summary>
    public string SecretKey { get; set; } = null!;

    public int RecvWindow { get; set; } = 60000;
}