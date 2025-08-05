using Crypto.Compare.Adapter.Config;

namespace Crypto.Compare.Adapter;

/// <summary>
/// Basic adapter for each market
/// </summary>
public interface IAdapter
{
    /// <summary>
    /// Name market adapter
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Adapter turn on for work
    /// </summary>
    bool Enabled { get; }

    /// <summary>
    /// List current enable symbols for book
    /// </summary>
    ICollection<string> GetSymbols();

    /// <summary>
    /// Get config
    /// </summary>
    BaseConfig Config { get; }
}
