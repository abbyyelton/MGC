using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MGC.Migrations
{
    public partial class SetUniqueFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Recipients",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_Name",
                table: "Recipients",
                column: "Name",
                unique: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Holidays",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_Name",
                table: "Holidays",
                column: "Name",
                unique: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Gifts",
                type: "decimal(7,2)",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recipients_Name",
                table: "Recipients");

            migrationBuilder.DropIndex(
                name: "IX_Holidays_Name",
                table: "Holidays");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Recipients",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Holidays",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Gifts",
                nullable: false);
        }
    }
}
