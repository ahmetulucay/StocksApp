﻿namespace StocksApp.Services;
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
                RequestUri = new Uri("url"),
                Method = HttpMethod.Get
            };

            HttpResponseMessage httpResResponseMessage = await httpClient.SendAsync(httpRequestMessage);
        }
    }
}