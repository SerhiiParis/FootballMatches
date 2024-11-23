namespace FootballMatches.Services.DataApi.Helpers;

/// <summary>
/// Odds feature is not included in free subscription
/// </summary>
public static class OddsGenerator
{
    private static readonly Random Random = new();
    
    public static decimal Get()
    {
        var probability = Random.NextDouble() * 0.5 + 0.25;
        return Math.Round((decimal)(1 / probability), 2);
    }
}