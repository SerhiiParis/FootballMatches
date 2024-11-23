using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballMatches.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Crestadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AwayTeamCrestUrl",
                table: "Match",
                type: "NVARCHAR(256)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeTeamCrestUrl",
                table: "Match",
                type: "NVARCHAR(256)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AwayTeamCrestUrl",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "HomeTeamCrestUrl",
                table: "Match");
        }
    }
}
