using StocksApp.ServiceContracts;
using System.Text.Json;

namespace StocksApp.Services;
public class FinnhubService : IFinnhubService
{
    private readonly IHttpClientFactory _httpClientFactory;
    public FinnhubService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Dictionary<string, object>> GetStockPriceQuote()
    {
        using (HttpClient httpClient = _httpClientFactory.CreateClient())
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri("https://finnhub.io/api/v1/quote?symbol=MSFT&token=ch6bt31r01qo6f5d9aigch6bt31r01qo6f5d9aj0"),
                Method = HttpMethod.Get
            };

            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            Stream stream = httpResponseMessage.Content.ReadAsStream();
            StreamReader streamReader = new StreamReader(stream);

            string response = streamReader.ReadToEnd();

            Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

            if (responseDictionary == null) 
            {
                throw new InvalidOperationException("No response from finnhub server");
            }
            if (responseDictionary.ContainsKey("error"))
            {
                throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));
            }

            return responseDictionary;
        }
    }

    public Dictionary<string, object> GetStockPriceQuote(string stockSymbol)
    {
        throw new NotImplementedException();
    }
}
