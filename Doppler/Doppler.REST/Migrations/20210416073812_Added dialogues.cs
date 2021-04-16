using Microsoft.EntityFrameworkCore.Migrations;

namespace Doppler.REST.Migrations
{
    public partial class Addeddialogues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Conversations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "FirstUserId",
                table: "Conversations",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SecondUserId",
                table: "Conversations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_FirstUserId",
                table: "Conversations",
                column: "FirstUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_SecondUserId",
                table: "Conversations",
                column: "SecondUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_ConversationMembers_FirstUserId",
                table: "Conversations",
                column: "FirstUserId",
                principalTable: "ConversationMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_ConversationMembers_SecondUserId",
                table: "Conversations",
                column: "SecondUserId",
                principalTable: "ConversationMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_ConversationMembers_FirstUserId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_ConversationMembers_SecondUserId",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_FirstUserId",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_SecondUserId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "FirstUserId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "SecondUserId",
                table: "Conversations");
        }
    }
}
