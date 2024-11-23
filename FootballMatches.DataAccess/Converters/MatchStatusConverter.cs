using FootballMatches.Models.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FootballMatches.DataAccess.Converters
{
    public class MatchStatusConverter : ValueConverter<MatchStatus, string>
    {
        public static IReadOnlyDictionary<string, MatchStatus> LeagueCodeToLeague { get; } =
            new Dictionary<string, MatchStatus>
            {
                { "SCHEDULED ", MatchStatus.Scheduled },
                { "LIVE      ", MatchStatus.Live },
                { "INPLAY    ", MatchStatus.InPlay },
                { "PAUSED    ", MatchStatus.Paused },
                { "FINISHED  ", MatchStatus.Finished },
                { "POSTPONED ", MatchStatus.Postponed },
                { "SUSPENDED ", MatchStatus.Suspended },
                { "CANCELLED ", MatchStatus.Cancelled }
            };

        public static IReadOnlyDictionary<MatchStatus, string> LeagueToLeagueCode { get; } =
            LeagueCodeToLeague.ToDictionary(
                keyValuePair => keyValuePair.Value,
                keyValuePair => keyValuePair.Key);

        public MatchStatusConverter()
            : base(dealStatus => LeagueToLeagueCode[dealStatus],
                   dealStatusCode => LeagueCodeToLeague[dealStatusCode])
        {
        }
    }
}
