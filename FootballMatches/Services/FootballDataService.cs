using FootballMatches.Models;

namespace FootballMatches.Services;

public class FootballDataService
{
    public List<League> GetRecentLeagues()
    {
        return new List<League>
        {
            new League
            {
                Name = "A",
                Matches = new List<Match>
                {
                    new Match { HomeTeam = "Team A", AwayTeam = "Team B", Date = DateTime.Now, Location = "Stadium X", Odds1 = 1.5M, Odds2 = 2.5M, OddsDraw = 1.0M },
                    new Match { HomeTeam = "Team C", AwayTeam = "Team D", Date = DateTime.Now.AddHours(2), Location = "Stadium Y", Odds1 = 2.0M, Odds2 = 3.0M, OddsDraw = 4.0M }
                }
            },
            new League
            {
                Name = "B",
                Matches = new List<Match>
                {
                    new Match { HomeTeam = "Team A123", AwayTeam = "Team B", Date = DateTime.Now, Location = "Stadium X", Odds1 = 1.5M, Odds2 = 2.5M, OddsDraw = 1.0M },
                    new Match { HomeTeam = "Team C123", AwayTeam = "Team D", Date = DateTime.Now.AddHours(2), Location = "Stadium Y", Odds1 = 2.0M, Odds2 = 3.0M, OddsDraw = 4.0M }
                }
            }
        };
    }

    public List<League> GetUpcomingLeagues()
    {
        return new List<League>
        {
            new League
            {
                Name = "A",
                Matches = new List<Match>
                {
                    new Match { HomeTeam = "Team A", AwayTeam = "Team B", Date = DateTime.Now, Location = "Stadium X", Odds1 = 1.5M, Odds2 = 2.5M, OddsDraw = 1.0M },
                    new Match { HomeTeam = "Team C", AwayTeam = "Team D", Date = DateTime.Now.AddHours(2), Location = "Stadium Y", Odds1 = 2.0M, Odds2 = 3.0M, OddsDraw = 4.0M }
                }
            },
            new League
            {
                Name = "B",
                Matches = new List<Match>
                {
                    new Match { HomeTeam = "Team A123", AwayTeam = "Team B", Date = DateTime.Now, Location = "Stadium X", Odds1 = 1.5M, Odds2 = 2.5M, OddsDraw = 1.0M },
                    new Match { HomeTeam = "Team C123", AwayTeam = "Team D", Date = DateTime.Now.AddHours(2), Location = "Stadium Y", Odds1 = 2.0M, Odds2 = 3.0M, OddsDraw = 4.0M }
                }
            }
        };
    }
}
