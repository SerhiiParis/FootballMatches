namespace FootballMatches.Services;

public class ApplicationConfig
{
    public List<string> Competitions { get; set; } = null!;
    public int MaxRecentHoursToleranceHours { get; set; }
    public int  MaxUpcomingHoursToleranceHours { get; set; }
}