namespace StocksApp.Services;
public class MyService
{
    private readonly IHttpClientFactory _httpClientFactory;
    public MyService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task method()
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
        }
    }
}
