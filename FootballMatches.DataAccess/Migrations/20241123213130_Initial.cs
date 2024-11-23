using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballMatches.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApiId = table.Column<int>(type: "INT", nullable: false),
                    League = table.Column<string>(type: "TEXT", nullable: false),
                    HomeTeam = table.Column<string>(type: "NVARCHAR(256)", nullable: false),
                    AwayTeam = table.Column<string>(type: "NVARCHAR(256)", nullable: false),
                    Date = table.Column<long>(type: "DATETIMEOFFSET (7)", nullable: false),
                    Location = table.Column<string>(type: "NVARCHAR(500)", nullable: false),
                    HomeWin = table.Column<decimal>(type: "DECIMAL (19, 4)", nullable: true),
                    Draw = table.Column<decimal>(type: "DECIMAL (19, 4)", nullable: true),
                    AwayWin = table.Column<decimal>(type: "DECIMAL (19, 4)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Match");
        }
    }
}
