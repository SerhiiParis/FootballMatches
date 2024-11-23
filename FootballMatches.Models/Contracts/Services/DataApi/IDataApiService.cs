using FootballMatches.Models.Dto;
using FootballMatches.Models.Enums;

namespace FootballMatches.Models.Contracts.Services.DataApi;

public interface IDataApiService
{
    Task<List<MatchDto>> GetMatches(DateTimeOffset dateFrom, DateTimeOffset dateTo, List<MatchStatus> statuses);
}