using System.ComponentModel;

// ReSharper disable InconsistentNaming

namespace FootballMatches.Models.Enums;

public enum League
{
    [Description("Premier League (England)")]
    PL,
    [Description("Primeira Liga (Portugal)")]
    PPL,
    [Description("Eredivisie (Netherlands)")]
    DED,
    [Description("Bundesliga (Germany)")]
    BL1,
    [Description("Serie A (Italy)")]
    SA,
    [Description("Primera Division (Spain)")]
    PD,
    [Description("Campeonato Brasileiro Série A (Brazil)")]
    BSA
}
