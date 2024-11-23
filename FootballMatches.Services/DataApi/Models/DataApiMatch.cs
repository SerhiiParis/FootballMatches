namespace FootballMatches.Services.DataApi.Models;

public class DataApiMatch
{
    public required int Id { get; set; }
    public required DateTimeOffset UtcDate { get; set; }
    
    public required DataApiMatchNameObj Area { get; set; }
    public required DataApiMatchCompetition Competition { get; set; }
    public required DataApiMatchNameObj HomeTeam { get; set; }
    public required DataApiMatchNameObj AwayTeam { get; set; }

    // public required MatchOdds Odds { get; set; } - paid subscription feature
}

public class DataApiMatchNameObj
{
    public required string Name { get; set; }
}

public class DataApiMatchCompetition : DataApiMatchNameObj
{
    public required string Code { get; set; }
}