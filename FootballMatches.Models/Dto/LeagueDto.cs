namespace FootballMatches.Models.Dto;

public class LeagueDto
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    
    public required List<MatchDto> Matches { get; set; }
}