namespace StocksApp.ServiceContracts;
public interface IFinnhubService
{
    Dictionary<string, object> GetStockPriceQuote(string stockSymbol);
}
