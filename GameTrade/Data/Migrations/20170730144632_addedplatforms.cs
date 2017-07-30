using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GameTrade.Data.Migrations
{
    public partial class addedplatforms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlatformId",
                table: "Games",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_PlatformId",
                table: "Games",
                column: "PlatformId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Platforms_PlatformId",
                table: "Games",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Platforms_PlatformId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "Platforms");

            migrationBuilder.DropIndex(
                name: "IX_Games_PlatformId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "Games");
        }
    }
}
