using System.ComponentModel.DataAnnotations;
using FootballMatches.Models.Enums;

namespace FootballMatches.Models;

public class Match
{
    [Required]
    public int Id { get; set; }

    [Required]
    public required int ApiId { get; set; }

    [Required]
    public required League League { get; set; }
    
    [Required]
    public required MatchStatus Status { get; set; }

    [Required]
    public required string HomeTeam { get; set; }
    
    [Required]
    public required string HomeTeamCrestUrl { get; set; }

    [Required]
    public required string AwayTeam { get; set; }
    
    [Required]
    public required string AwayTeamCrestUrl { get; set; }

    [Required]
    public required DateTimeOffset Date { get; set; }
    
    [Required]
    public required string Location { get; set; }
    
    public decimal? HomeWin { get; set; }
    public decimal? Draw { get; set; }
    public decimal? AwayWin { get; set; }
}