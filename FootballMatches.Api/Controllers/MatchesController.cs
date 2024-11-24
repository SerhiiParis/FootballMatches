using System.Net;
using FootballMatches.Models.Contracts.Services;
using FootballMatches.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FootballMatches.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchesController : ControllerBase
{
    private readonly IMatchService _matchService;

    public MatchesController(IMatchService matchService)
    {
        _matchService = matchService;
    }

    [HttpGet("recent")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<LeagueDto>))]
    public async Task<List<LeagueDto>> GetRecent()
    {
        return await _matchService.GetRecent();
    }

    [HttpGet("upcoming")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<LeagueDto>))]
    public async Task<List<LeagueDto>> GetUpcoming()
    {
        return await _matchService.GetUpcoming();
    }
}