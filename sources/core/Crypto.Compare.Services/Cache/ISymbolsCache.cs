using Crypto.Compare.Data;

namespace Crypto.Compare.Services.Cache;

/// <summary>
/// Work with data from cache
/// </summary>
public interface ISymbolsCache
{
    /// <summary>
    /// Get data by Id
    /// </summary>
    List<SymbolProvider> GetByProvider(int id);
    
    /// <summary>
    /// Get data by provider name name 
    /// </summary>
    List<SymbolProvider> GetByProvider(string name);
    
    /// <summary>
    /// Get data by ticker 
    /// </summary>
    List<SymbolProvider> GetByTicker(string ticker);
}