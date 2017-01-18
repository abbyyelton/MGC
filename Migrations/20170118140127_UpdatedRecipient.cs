using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MGC.Migrations
{
    public partial class UpdatedRecipient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GiftUserId",
                table: "Recipients",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_GiftUserId",
                table: "Recipients",
                column: "GiftUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipients_AspNetUsers_GiftUserId",
                table: "Recipients",
                column: "GiftUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipients_AspNetUsers_GiftUserId",
                table: "Recipients");

            migrationBuilder.DropIndex(
                name: "IX_Recipients_GiftUserId",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "GiftUserId",
                table: "Recipients");
        }
    }
}
