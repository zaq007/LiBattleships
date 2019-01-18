using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LiBattleship.Shared.Infrastructure.Migrations
{
    public partial class RemoveWL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameHistories_AspNetUsers_LoserId",
                table: "GameHistories");

            migrationBuilder.RenameColumn(
                name: "WinnerField",
                table: "GameHistories",
                newName: "Player2Field");

            migrationBuilder.RenameColumn(
                name: "LoserId",
                table: "GameHistories",
                newName: "Player2Id");

            migrationBuilder.RenameColumn(
                name: "LoserField",
                table: "GameHistories",
                newName: "Player1Field");

            migrationBuilder.RenameIndex(
                name: "IX_GameHistories_LoserId",
                table: "GameHistories",
                newName: "IX_GameHistories_Player2Id");

            migrationBuilder.AddColumn<Guid>(
                name: "Player1Id",
                table: "GameHistories",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_GameHistories_Player1Id",
                table: "GameHistories",
                column: "Player1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameHistories_AspNetUsers_Player1Id",
                table: "GameHistories",
                column: "Player1Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameHistories_AspNetUsers_Player2Id",
                table: "GameHistories",
                column: "Player2Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameHistories_AspNetUsers_Player1Id",
                table: "GameHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_GameHistories_AspNetUsers_Player2Id",
                table: "GameHistories");

            migrationBuilder.DropIndex(
                name: "IX_GameHistories_Player1Id",
                table: "GameHistories");

            migrationBuilder.DropColumn(
                name: "Player1Id",
                table: "GameHistories");

            migrationBuilder.RenameColumn(
                name: "Player2Id",
                table: "GameHistories",
                newName: "LoserId");

            migrationBuilder.RenameColumn(
                name: "Player2Field",
                table: "GameHistories",
                newName: "WinnerField");

            migrationBuilder.RenameColumn(
                name: "Player1Field",
                table: "GameHistories",
                newName: "LoserField");

            migrationBuilder.RenameIndex(
                name: "IX_GameHistories_Player2Id",
                table: "GameHistories",
                newName: "IX_GameHistories_LoserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameHistories_AspNetUsers_LoserId",
                table: "GameHistories",
                column: "LoserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
