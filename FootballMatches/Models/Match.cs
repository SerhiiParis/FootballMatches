namespace FootballMatches.Models;

public class Match
{
    public string? HomeTeam { get; set; }
    public string? AwayTeam { get; set; }
    public DateTime Date { get; set; }
    public string? Location { get; set; }
    public decimal? Odds1 { get; set; }
    public decimal? OddsDraw { get; set; }
    public decimal? Odds2 { get; set; }
}