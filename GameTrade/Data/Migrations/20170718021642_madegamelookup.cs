using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameTrade.Data.Migrations
{
    public partial class madegamelookup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Games",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "GameID",
                table: "Games",
                newName: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Games",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "Games",
                newName: "GameID");
        }
    }
}
