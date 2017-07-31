using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameTrade.Data.Migrations
{
    public partial class removedpropsfromgameclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameCondition",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "System",
                table: "Games");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GameCondition",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "System",
                table: "Games",
                nullable: true);
        }
    }
}
