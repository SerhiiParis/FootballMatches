using System.Web;
using FootballMatches.Models.Contracts.Services.DataApi;
using FootballMatches.Models.Enums;
using FootballMatches.Services.DataApi.Helpers;
using FootballMatches.Shared;
using Microsoft.Extensions.Options;

namespace FootballMatches.Services.DataApi;

public class DataApiClient : IDataApiClient
{
    private readonly ApplicationConfig _applicationConfig;
    private readonly DataApiConfig _apiConfig;
    private readonly HttpClient _httpClient;

    public DataApiClient(
        IOptions<ApplicationConfig> applicationConfig,
        IOptions<DataApiConfig> apiConfig,
        HttpClient httpClient)
    {
        _applicationConfig = applicationConfig.Value;
        _apiConfig = apiConfig.Value;
        _httpClient = httpClient;

        _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", _apiConfig.ApiKey);
    }

    public async Task<HttpResponseMessage> Get(
        DateTimeOffset dateFrom,
        DateTimeOffset dateTo,
        List<MatchStatus> statuses)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query[nameof(dateFrom)] = dateFrom.UtcDateTime.ToString(Constants.DateFormat);
        query[nameof(dateTo)] = dateTo.UtcDateTime.ToString(Constants.DateFormat);
        query["competitions"] = string.Join(Constants.Comma, _applicationConfig.Competitions);
        query["status"] = string.Join(Constants.Comma, statuses.Select(x => x.GetDescriptionString()));

        var builder = new UriBuilder(_apiConfig.Url + _apiConfig.GetMatchesRoute)
        {
            Query = query.ToString()
        };

        return await _httpClient.GetAsync(builder.ToString());
    }
}