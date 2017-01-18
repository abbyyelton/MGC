using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MGC.Migrations
{
    public partial class UpdatedGiftsWithGiftUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Gifts");

            migrationBuilder.AddColumn<string>(
                name: "GiftUserId",
                table: "Gifts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_GiftUserId",
                table: "Gifts",
                column: "GiftUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gifts_AspNetUsers_GiftUserId",
                table: "Gifts",
                column: "GiftUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gifts_AspNetUsers_GiftUserId",
                table: "Gifts");

            migrationBuilder.DropIndex(
                name: "IX_Gifts_GiftUserId",
                table: "Gifts");

            migrationBuilder.DropColumn(
                name: "GiftUserId",
                table: "Gifts");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Gifts",
                nullable: true);
        }
    }
}
