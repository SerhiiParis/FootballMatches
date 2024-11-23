namespace FootballMatches.Models.Contracts.DataAccess;

public interface IMatchRepository
{
    Task<List<Match>> Get(DateTimeOffset from, DateTimeOffset to);
    void Add(List<Match> matches);
    Task Save();
}