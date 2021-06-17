using Microsoft.EntityFrameworkCore.Migrations;

namespace Doppler.REST.Migrations
{
    public partial class Removedmessagereferencefrommessagecontent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConversationMessageContents_ConversationMessages_Id",
                table: "ConversationMessageContents");

            migrationBuilder.DropColumn(
                name: "AccessLevel",
                table: "Files");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationMessages_ContentId",
                table: "ConversationMessages",
                column: "ContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationMessages_ConversationMessageContents_ContentId",
                table: "ConversationMessages",
                column: "ContentId",
                principalTable: "ConversationMessageContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConversationMessages_ConversationMessageContents_ContentId",
                table: "ConversationMessages");

            migrationBuilder.DropIndex(
                name: "IX_ConversationMessages_ContentId",
                table: "ConversationMessages");

            migrationBuilder.AddColumn<int>(
                name: "AccessLevel",
                table: "Files",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationMessageContents_ConversationMessages_Id",
                table: "ConversationMessageContents",
                column: "Id",
                principalTable: "ConversationMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
