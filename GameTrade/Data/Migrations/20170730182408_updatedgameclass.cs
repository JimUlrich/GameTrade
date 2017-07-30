using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameTrade.Data.Migrations
{
    public partial class updatedgameclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Designation",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "ConditionId",
                table: "Games",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DesignationId",
                table: "Games",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Games_ConditionId",
                table: "Games",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_DesignationId",
                table: "Games",
                column: "DesignationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Conditions_ConditionId",
                table: "Games",
                column: "ConditionId",
                principalTable: "Conditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Designations_DesignationId",
                table: "Games",
                column: "DesignationId",
                principalTable: "Designations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Conditions_ConditionId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Designations_DesignationId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_ConditionId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_DesignationId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ConditionId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "DesignationId",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "Games",
                nullable: true);
        }
    }
}
