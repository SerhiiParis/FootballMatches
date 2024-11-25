using FootballMatches.Models;
using FootballMatches.Models.Contracts.DataAccess;
using FootballMatches.Models.Contracts.Services;
using FootballMatches.Models.Contracts.Services.DataApi;
using FootballMatches.Models.Dto;
using FootballMatches.Models.Enums;
using FootballMatches.Shared;
using Microsoft.Extensions.Options;

namespace FootballMatches.Services.Services;

public class MatchService : IMatchService
{
    private readonly ApplicationConfig _appConfig;
    private readonly IDataApiService _apiService;
    private readonly IMatchRepository _matchRepository;

    private static DateTimeOffset? _lastRecentPull = null; // should be replaced with Redis
    private static DateTimeOffset? _lastUpcomingPull = null; // should be replaced with Redis

    public MatchService(
        IOptions<ApplicationConfig> appConfig,
        IDataApiService apiService,
        IMatchRepository matchRepository)
    {
        _appConfig = appConfig.Value;
        _apiService = apiService;
        _matchRepository = matchRepository;
    }

    public async Task<List<LeagueDto>> GetRecent()
    {
        var to = DateTimeOffset.UtcNow.Date;
        var from = to.AddDays(-7).Date;
        
        var result = await Get(
            from, to, [MatchStatus.Finished], _lastRecentPull,
            _appConfig.MaxRecentHoursToleranceHours, orderByDescending: true);
       
        _lastRecentPull = DateTimeOffset.UtcNow;
        return result;
    }

    public async Task<List<LeagueDto>> GetUpcoming()
    {
        var from = DateTimeOffset.UtcNow.Date;
        var to = from.AddDays(7).Date;
        
        var result = await Get(
            from, to, [MatchStatus.Scheduled], _lastUpcomingPull,
            _appConfig.MaxUpcomingHoursToleranceHours);
        
        _lastUpcomingPull = DateTimeOffset.UtcNow;
        return result;
    }

    private async Task<List<LeagueDto>> Get(
        DateTimeOffset from,
        DateTimeOffset to,
        List<MatchStatus> statuses,
        DateTimeOffset? lastPull,
        int maxUpcomingHoursTolerance,
        bool orderByDescending = false)
    {
        var repoMatches = await _matchRepository.Get(from, to, statuses);

        if (lastPull == null || 
            repoMatches.Count == 0 ||
            DateTimeOffset.UtcNow - lastPull > TimeSpan.FromHours(maxUpcomingHoursTolerance))
        {
            var newMatches = await AddMatches(from, to, statuses, repoMatches);
            await _matchRepository.Save();
            repoMatches = repoMatches.Union(newMatches).ToList();
        }

        var repoMatchesQ = orderByDescending
            ? repoMatches.OrderByDescending(r => r.Date)
            : repoMatches.OrderBy(r => r.Date);

        return repoMatchesQ.GroupBy(x => x.League).Select(grouped => new LeagueDto
        {
            Name = grouped.Key.GetDescriptionString(),
            Code = grouped.Key.ToString(),
            Matches = grouped.Select(match => new MatchDto
            {
                ApiId = match.ApiId,
                League = match.League,
                Status = match.Status,
                HomeTeam = match.HomeTeam,
                AwayTeam = match.AwayTeam,
                HomeTeamCrestUrl = match.HomeTeamCrestUrl,
                AwayTeamCrestUrl = match.AwayTeamCrestUrl,
                Date = match.Date,
                Location = match.Location,
                HomeWin = match.HomeWin,
                Draw = match.Draw,
                AwayWin = match.AwayWin
            }).ToList()
        }).ToList();
    }

    private async Task<List<Match>> AddMatches(
        DateTimeOffset from,
        DateTimeOffset to,
        List<MatchStatus> statuses,
        List<Match> repoMatches)
    {
        var apiMatches = await _apiService.GetMatches(from, to, statuses);

        var matchesToAdd = apiMatches
            .Where(a => repoMatches.All(r => r.ApiId != a.ApiId))
            .Select(x => new Match
            {
                ApiId = x.ApiId,
                League = x.League,
                Status = x.Status,
                HomeTeam = x.HomeTeam,
                AwayTeam = x.AwayTeam,
                HomeTeamCrestUrl = x.HomeTeamCrestUrl,
                AwayTeamCrestUrl = x.AwayTeamCrestUrl,
                Date = x.Date,
                Location = x.Location,
                HomeWin = x.HomeWin,
                Draw = x.Draw,
                AwayWin = x.AwayWin
            })
            .ToList();
        var matchesToUpdate = repoMatches.Where(r => apiMatches.Any(a => a.ApiId == r.ApiId));

        foreach (var match in matchesToUpdate)
        {
            var newMatch = apiMatches.First(x => x.ApiId == match.ApiId);
            if (match.Location != newMatch.Location ||
                match.Date != newMatch.Date ||
                match.Status != newMatch.Status //||
                // match.HomeWin != newMatch.HomeWin ||  // when real data will be available - uncomment
                // match.Draw != newMatch.Draw ||
                /*match.AwayWin != newMatch.AwayWin*/ )
            {
                match.Location = newMatch.Location;
                match.Date = newMatch.Date;
                match.Status = newMatch.Status;
                // match.HomeWin = newMatch.HomeWin;
                // match.Draw = newMatch.Draw;
                // match.AwayWin = newMatch.AwayWin;
            }
        }

        _matchRepository.Add(matchesToAdd);
        return matchesToAdd;
    }
}
