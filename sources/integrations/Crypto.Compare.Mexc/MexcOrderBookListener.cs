using Mexc.Net.Interfaces;

namespace Crypto.Compare.Mexc;

public class MexcOrderBookListener
{
    private readonly IMexcOrderBookFactory _mexcOrderBookFactory;

    public MexcOrderBookListener(IMexcOrderBookFactory mexcOrderBookFactory)
    {
        _mexcOrderBookFactory = mexcOrderBookFactory;
    }
}