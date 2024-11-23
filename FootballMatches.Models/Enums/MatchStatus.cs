using System.ComponentModel;

namespace FootballMatches.Models.Enums;

public enum MatchStatus
{
    [Description("SCHEDULED")]
    Scheduled,
    [Description("LIVE")]
    Live,
    [Description("IN_PLAY")]
    InPlay,
    [Description("PAUSED")]
    Paused,
    [Description("FINISHED")]
    Finished,
    [Description("POSTPONED")]
    Postponed,
    [Description("SUSPENDED")]
    Suspended,
    [Description("CANCELLED")]
    Cancelled
}
