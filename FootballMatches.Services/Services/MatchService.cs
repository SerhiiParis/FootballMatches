using FootballMatches.Models;
using FootballMatches.Models.Contracts.DataAccess;
using FootballMatches.Models.Contracts.Services;
using FootballMatches.Models.Contracts.Services.DataApi;
using FootballMatches.Models.Dto;
using FootballMatches.Shared;
using Microsoft.Extensions.Options;

namespace FootballMatches.Services.Services;

public class MatchService : IMatchService
{
    private readonly ApplicationConfig _appConfig;
    private readonly IDataApiService _apiService;
    private readonly IMatchRepository _matchRepository;

    private static DateTimeOffset? _lastRecentPull = null;
    private static DateTimeOffset? _lastUpcomingPull = null;

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
        var to = DateTimeOffset.UtcNow;
        var from = to.AddDays(-10);
        
        var result = await Get(from, to, _lastRecentPull, _appConfig.MaxRecentHoursToleranceHours);
       
        _lastRecentPull = DateTimeOffset.UtcNow;
        return result;
    }

    public async Task<List<LeagueDto>> GetUpcoming()
    {
        var from = DateTimeOffset.UtcNow;
        var to = from.AddDays(10);
        
        var result = await Get(from, to, _lastUpcomingPull, _appConfig.MaxUpcomingHoursToleranceHours);
        
        _lastUpcomingPull = DateTimeOffset.UtcNow;
        return result;
    }

    private async Task<List<LeagueDto>> Get(
        DateTimeOffset from,
        DateTimeOffset to,
        DateTimeOffset? lastPull,
        int maxUpcomingHoursTolerance)
    {
        var repoMatches = await _matchRepository.Get(from, to);

        if (lastPull == null || 
            repoMatches.Count == 0 ||
            DateTimeOffset.UtcNow - lastPull > TimeSpan.FromHours(maxUpcomingHoursTolerance))
        {
            from = repoMatches.Count == 0 ? from : repoMatches.Max(x => x.Date);
            var apiMatches = await _apiService.GetMatches(from, to);

            var newRepoMatches = apiMatches.Select(x => new Match
            {
                ApiId = x.ApiId,
                League = x.League,
                HomeTeam = x.HomeTeam,
                AwayTeam = x.AwayTeam,
                Date = x.Date,
                Location = x.Location,
                HomeWin = x.HomeWin,
                Draw = x.Draw,
                AwayWin = x.AwayWin
            }).ToList();

            await _matchRepository.Save(newRepoMatches);
            repoMatches = repoMatches
                .Union(newRepoMatches)
                .DistinctBy(x => x.ApiId)
                .ToList();
        }

        return repoMatches.GroupBy(x => x.League).Select(grouped => new LeagueDto
        {
            Name = grouped.Key.GetDescriptionString(),
            Code = grouped.Key.ToString(),
            Matches = grouped.Select(match => new MatchDto
            {
                ApiId = match.ApiId,
                League = match.League,
                HomeTeam = match.HomeTeam,
                AwayTeam = match.AwayTeam,
                Date = match.Date,
                Location = match.Location,
                HomeWin = match.HomeWin,
                Draw = match.Draw,
                AwayWin = match.AwayWin
            }).ToList()
        }).ToList();
    }
}
