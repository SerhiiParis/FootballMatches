namespace FootballMatches.Services.DataApi.Models;

public class DataApiMatch
{
    public required int Id { get; set; }
    public required DateTimeOffset UtcDate { get; set; }
    public required string Status { get; set; }
    
    public required DataApiMatchNameArea Area { get; set; }
    public required DataApiMatchCompetition Competition { get; set; }
    public required DataApiMatchTeam HomeTeam { get; set; }
    public required DataApiMatchTeam AwayTeam { get; set; }

    // public required MatchOdds Odds { get; set; } - paid subscription feature
}

public class DataApiMatchNameArea
{
    public required string Name { get; set; }
}

public class DataApiMatchCompetition
{
    public required string Name { get; set; }
    public required string Code { get; set; }
}

public class DataApiMatchTeam
{
    public required string Name { get; set; }
    public required string Crest { get; set; }
}
