namespace FootballMatches.Models.Contracts.DataAccess;

public interface IMatchRepository
{
    Task<List<Match>> Get(DateTimeOffset from, DateTimeOffset to);

    Task Save(List<Match> matches);
}