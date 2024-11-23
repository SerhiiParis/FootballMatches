namespace FootballMatches.Services.DataApi;

public class DataApiConfig
{
    public string ApiKey { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string GetMatchesRoute { get; set; } = null!;
}