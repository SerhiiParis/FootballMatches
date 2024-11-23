using FootballMatches.Models;
using FootballMatches.Models.Contracts.DataAccess;
using FootballMatches.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace FootballMatches.DataAccess.Repositories;

public class MatchRepository : IMatchRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MatchRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Match>> Get(DateTimeOffset from, DateTimeOffset to, List<MatchStatus> statuses)
    {
        return await _dbContext.Matches
            .Where(x => x.Date >= from && x.Date <= to && statuses.Contains(x.Status))
            .ToListAsync();
    }

    public void Add(List<Match> matches)
    {
        _dbContext.Matches.AddRange(matches);
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }
}
