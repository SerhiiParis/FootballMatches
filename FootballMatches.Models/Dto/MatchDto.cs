using FootballMatches.Models.Enums;

namespace FootballMatches.Models.Dto;

public class MatchDto
{
    public required int ApiId { get; set; }
    
    public required League League { get; set; }
    public required string HomeTeam { get; set; }
    public required string AwayTeam { get; set; }
    public required DateTimeOffset Date { get; set; }
    public required string Location { get; set; }
    
    public decimal? HomeWin { get; set; }
    public decimal? Draw { get; set; }
    public decimal? AwayWin { get; set; }
}