using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FootballMatches.Models;
using FootballMatches.Services;

namespace FootballMatches.Controllers;

public class HomeController : Controller
{
    private readonly FootballDataService _service;
    private readonly ILogger<HomeController> _logger;

    public HomeController(
        FootballDataService service,
        ILogger<HomeController> logger)
    {
        _service = service;
        _logger = logger;
    }

    public IActionResult Index(string view = "Recent")
    {
        var matches = view == "Upcoming" ? _service.GetUpcomingLeagues() : _service.GetRecentLeagues();
        ViewData["ViewType"] = view;
        return View(matches);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}