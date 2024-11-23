using FootballMatches.Models.Dto;

namespace FootballMatches.Models.Contracts.Services;

public interface IMatchService
{
    Task<List<LeagueDto>> GetRecent();
    Task<List<LeagueDto>> GetUpcoming();
}