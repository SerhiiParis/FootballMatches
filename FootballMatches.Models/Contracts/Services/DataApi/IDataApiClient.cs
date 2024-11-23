namespace FootballMatches.Models.Contracts.Services.DataApi;

public interface IDataApiClient
{
    Task<HttpResponseMessage> Get(DateTimeOffset dateFrom, DateTimeOffset dateTo);
}