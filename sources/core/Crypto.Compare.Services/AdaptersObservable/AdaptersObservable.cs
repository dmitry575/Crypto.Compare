using Crypto.Compare.Data;
using Crypto.Compare.Services.Cache;

namespace Crypto.Compare.Services.AdaptersObservable;

public class AdaptersObservable : IObserver<SymbolProvider>
{
    private readonly IEnumerable<IObserver<SymbolProvider>> _observers;
    private readonly ISymbolsCache _symbolsCache;
    
    public AdaptersObservable(IEnumerable<IObserver<SymbolProvider>> observers, ISymbolsCache symbolsCache)
    {
        _observers = observers;
        _symbolsCache = symbolsCache;
    }

    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Catch message of new price from provider 
    /// </summary>
    /// <param name="value"></param>
    public void OnNext(SymbolProvider value)
    {
        throw new NotImplementedException();
    }
}