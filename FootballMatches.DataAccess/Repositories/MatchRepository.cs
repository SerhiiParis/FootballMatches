﻿using FootballMatches.Models;
using FootballMatches.Models.Contracts.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace FootballMatches.DataAccess.Repositories;

public class MatchRepository : IMatchRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MatchRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Match>> Get(DateTimeOffset from, DateTimeOffset to)
    {
        return await _dbContext.Matches
            .Where(x => x.Date >= from && x.Date <= to)
            .ToListAsync();
    }

    public async Task Save(List<Match> matches)
    {
        _dbContext.Matches.AddRange(matches);
        await _dbContext.SaveChangesAsync();
    }
}