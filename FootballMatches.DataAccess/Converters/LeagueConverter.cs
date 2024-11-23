using FootballMatches.Models.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FootballMatches.DataAccess.Converters
{
    public class LeagueConverter : ValueConverter<League, string>
    {
        public static IReadOnlyDictionary<string, League> LeagueCodeToLeague { get; } =
            new Dictionary<string, League>
            {
                { "PL ", League.PL },
                { "PPL", League.PPL },
                { "DED", League.DED },
                { "BL1", League.BL1 },
                { "SA ", League.SA },
                { "PD ", League.PD },
                { "BSA", League.BSA }
            };

        public static IReadOnlyDictionary<League, string> LeagueToLeagueCode { get; } =
            LeagueCodeToLeague.ToDictionary(
                keyValuePair => keyValuePair.Value,
                keyValuePair => keyValuePair.Key);

        public LeagueConverter()
            : base(dealStatus => LeagueToLeagueCode[dealStatus],
                   dealStatusCode => LeagueCodeToLeague[dealStatusCode])
        {
        }
    }
}
