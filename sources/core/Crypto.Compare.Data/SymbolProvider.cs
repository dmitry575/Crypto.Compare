namespace Crypto.Compare.Data;

public class SymbolProvider
{
    public long Id { get; set; }
    public string Ticker { get; set; }
    public string Symbol { get; set; }
    public string ProviderName { get; set; }
    public decimal PriceSell { get; set; }
    public decimal PriceBuy { get; set; }
    public DateTime UpdatedAt { get; set; }
}