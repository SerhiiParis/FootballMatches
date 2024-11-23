using FootballMatches.Models.Dto;

namespace FootballMatches.Models.Contracts.Services.DataApi;

public interface IDataApiService
{
    Task<List<MatchDto>> GetMatches(DateTimeOffset dateFrom, DateTimeOffset dateTo);
}