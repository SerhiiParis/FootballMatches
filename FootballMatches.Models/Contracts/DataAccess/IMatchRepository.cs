using FootballMatches.Models.Enums;

namespace FootballMatches.Models.Contracts.DataAccess;

public interface IMatchRepository
{
    Task<List<Match>> Get(DateTimeOffset from, DateTimeOffset to, List<MatchStatus> statuses);
    void Add(List<Match> matches);
    Task Save();
}