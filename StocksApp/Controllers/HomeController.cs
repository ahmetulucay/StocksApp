﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksApp.Services;
using StocksApp.Models;

namespace StocksApp.Controllers;
public class HomeController : Controller
{
    private readonly FinnhubService _finnhubService;
    private readonly ILogger<HomeController> _logger;
    private readonly IOptions<TradingOptions> _tradingOptions;
    public HomeController(FinnhubService finnhubService, ILogger<HomeController> logger, IOptions<TradingOptions> tradingOptions) 
    {
        _finnhubService = finnhubService;
        _logger = logger;
        _tradingOptions = tradingOptions;
    }

    [Route("/")]
    public async Task<IActionResult> Index()
    {
        if (_tradingOptions.Value.DefaultStockSymbol == null) _tradingOptions.Value.DefaultStockSymbol = "MSFT";

        Dictionary<string, object> responseDictionary =  await _finnhubService.GetStockPriceQuote(_tradingOptions.Value.DefaultStockSymbol);
        _logger.LogInformation("Getting Stock Prices");

        Stock stock = new Stock()
        {
            StockSymbol = _tradingOptions.Value.DefaultStockSymbol,
            CurrentPrice = Convert.ToDouble(responseDictionary["c"].ToString()),
            HighestPrice = Convert.ToDouble(responseDictionary["h"].ToString()),
            LowestPrice = Convert.ToDouble(responseDictionary["l"].ToString()),
            OpenPrice = Convert.ToDouble(responseDictionary["o"].ToString())
        };
        return View(stock);
    }
}
