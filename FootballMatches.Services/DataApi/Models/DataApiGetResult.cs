namespace FootballMatches.Services.DataApi.Models;

public class DataApiGetResult
{
    public required List<DataApiMatch> Matches { get; set; }
}