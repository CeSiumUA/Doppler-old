using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Doppler.REST.Migrations
{
    public partial class Correctingmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IconId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_IconId",
                table: "Users",
                column: "IconId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Files_IconId",
                table: "Users",
                column: "IconId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Files_IconId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_IconId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IconId",
                table: "Users");
        }
    }
}
