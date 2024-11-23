using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FootballMatches.Models;
using FootballMatches.Models.Contracts.Services;

namespace FootballMatches.Controllers;

public class HomeController : Controller
{
    private readonly IMatchService _matchService;

    public HomeController(
        IMatchService matchService)
    {
        _matchService = matchService;
    }

    public async Task<IActionResult> Index(string view = "Recent")
    {
        var leagues = view == "Upcoming" ? await _matchService.GetUpcoming() : await _matchService.GetRecent();
        ViewData["ViewType"] = view;
        return View(leagues);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}