using Microsoft.AspNetCore.Mvc;
using StocksApp.Services;

namespace StocksApp.Controllers;
public class HomeController : Controller
{
    private readonly FinnhubService _myService;
    private readonly ILogger<HomeController> _logger;
    public HomeController(FinnhubService myService, ILogger<HomeController> logger) 
    {
        _myService = myService;
        _logger = logger;
    }

    [Route("/")]
    public async Task<IActionResult> Index()
    {
        await _myService.GetStockPriceQuote();
        _logger.LogInformation("Getting Stock Prices");
        return View();
    }
}
