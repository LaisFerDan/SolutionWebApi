using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class CreateInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChessPlayers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    url = table.Column<string>(type: "varchar(70)", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    username = table.Column<string>(type: "varchar(40)", nullable: false),
                    followers = table.Column<int>(type: "int", nullable: false),
                    country = table.Column<string>(type: "varchar(3)", nullable: false),
                    last_online = table.Column<DateOnly>(type: "date", nullable: false),
                    joined = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChessPlayers", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChessPlayers");
        }
    }
}
