using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Shared.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trygrams",
                columns: table => new
                {
                    Key = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trygrams", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "TrygramValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TrygramKey = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrygramValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrygramValues_Trygrams_TrygramKey",
                        column: x => x.TrygramKey,
                        principalTable: "Trygrams",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrygramValues_TrygramKey",
                table: "TrygramValues",
                column: "TrygramKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrygramValues");

            migrationBuilder.DropTable(
                name: "Trygrams");
        }
    }
}
