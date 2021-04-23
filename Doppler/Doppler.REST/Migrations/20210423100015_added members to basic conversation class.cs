using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Doppler.REST.Migrations
{
    public partial class addedmemberstobasicconversationclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConversationMembers_Conversations_GroupId",
                table: "ConversationMembers");

            migrationBuilder.DropIndex(
                name: "IX_ConversationMembers_GroupId",
                table: "ConversationMembers");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "ConversationMembers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "ConversationMembers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConversationMembers_GroupId",
                table: "ConversationMembers",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationMembers_Conversations_GroupId",
                table: "ConversationMembers",
                column: "GroupId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
