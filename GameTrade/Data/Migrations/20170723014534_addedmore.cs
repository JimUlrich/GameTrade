using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameTrade.Data.Migrations
{
    public partial class addedmore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GamesdbID",
                table: "Games",
                newName: "Genre");

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "Games",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Designation",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "Games",
                newName: "GamesdbID");
        }
    }
}
