namespace Crypto.Compare.Adapter.Config;

public class BaseConfig
{
    /// <summary>
    /// Base url for requests
    /// </summary>
    public string? BaseUri { get; set; } = null!;

    /// <summary>
    /// Enabled adapter
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Uniq name adapter
    /// </summary>
    public string Name { get; set; } = null!;
}
