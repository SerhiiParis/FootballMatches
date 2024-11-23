using System.Text.Json;
using FootballMatches.Models.Contracts.Services.DataApi;
using FootballMatches.Models.Dto;
using FootballMatches.Models.Enums;
using FootballMatches.Services.DataApi.Helpers;
using FootballMatches.Services.DataApi.Models;
using Microsoft.Extensions.Logging;

namespace FootballMatches.Services.DataApi;

public class DataApiService : IDataApiService
{
    private readonly IDataApiClient _client;
    private readonly ILogger<DataApiService> _logger;

    private JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public DataApiService(
        IDataApiClient client,
        ILogger<DataApiService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<List<MatchDto>> GetMatches(DateTimeOffset dateFrom, DateTimeOffset dateTo)
    {
        var response = await _client.Get(dateFrom, dateTo);

        if (response.IsSuccessStatusCode)
        {
            var apiResult = JsonSerializer.Deserialize<DataApiGetResult>(
                await response.Content.ReadAsStringAsync(),
                _options);

            return apiResult!.Matches.Select(x => new MatchDto
            {
                ApiId = x.Id,
                League = Enum.Parse<League>(x.Competition.Code),
                HomeTeam = x.HomeTeam.Name,
                AwayTeam = x.AwayTeam.Name,
                HomeTeamCrestUrl = x.HomeTeam.Crest,
                AwayTeamCrestUrl = x.AwayTeam.Crest,
                Date = x.UtcDate,
                Location = x.Area.Name,
                HomeWin = OddsGenerator.Get(), // Odds feature is not included in free subscription
                Draw = OddsGenerator.Get(), // Odds feature is not included in free subscription
                AwayWin = OddsGenerator.Get() // Odds feature is not included in free subscription
            }).ToList();
        }

        var content = await response.Content.ReadAsStringAsync();
        var message = "FootballMatchesApi: Unable to get matches for date {0} to {1}. {2}";
        _logger.LogError(message, dateFrom, dateTo, content);
        throw new InvalidOperationException(string.Format(message, dateFrom, dateTo, content));
    }
}