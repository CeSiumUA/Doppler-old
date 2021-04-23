using Microsoft.EntityFrameworkCore.Migrations;

namespace Doppler.REST.Migrations
{
    public partial class Triedtofixedconstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_ConversationMembers_FirstUserId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_ConversationMembers_SecondUserId",
                table: "Conversations");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_ConversationMembers_FirstUserId",
                table: "Conversations",
                column: "FirstUserId",
                principalTable: "ConversationMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_ConversationMembers_SecondUserId",
                table: "Conversations",
                column: "SecondUserId",
                principalTable: "ConversationMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
