using FootballMatches.DataAccess.Converters;
using FootballMatches.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballMatches.DataAccess.Configurations
{
    internal class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder
                .ToTable(nameof(Match));

            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.League)
                .HasConversion(new LeagueConverter());

            builder
                .Property(c => c.ApiId)
                .HasColumnType("INT")
                .IsRequired();
            
            builder
                .Property(c => c.HomeTeam)
                .HasColumnType("NVARCHAR(256)")
                .IsRequired();
            
            builder
                .Property(c => c.HomeTeamCrestUrl)
                .HasColumnType("NVARCHAR(256)")
                .IsRequired();
            
            builder
                .Property(c => c.AwayTeam)
                .HasColumnType("NVARCHAR(256)")
                .IsRequired();
            
            builder
                .Property(c => c.AwayTeamCrestUrl)
                .HasColumnType("NVARCHAR(256)")
                .IsRequired();
            
            builder
                .Property(c => c.Date)
                .HasColumnType("DATETIMEOFFSET (7)")
                .IsRequired();
            
            builder
                .Property(c => c.Location)
                .HasColumnType("NVARCHAR(500)")
                .IsRequired();
            
            builder
                .Property(c => c.HomeWin)
                .HasColumnType("DECIMAL (19, 4)");
            
            builder
                .Property(c => c.Draw)
                .HasColumnType("DECIMAL (19, 4)");
            
            builder
                .Property(c => c.AwayWin)
                .HasColumnType("DECIMAL (19, 4)");
        }
    }
}
