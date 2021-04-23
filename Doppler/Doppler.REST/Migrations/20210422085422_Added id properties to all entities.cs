using Microsoft.EntityFrameworkCore.Migrations;

namespace Doppler.REST.Migrations
{
    public partial class Addedidpropertiestoallentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConversationMessages_ConversationMembers_ReceiverId",
                table: "ConversationMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ConversationMessages_ConversationMembers_SenderId",
                table: "ConversationMessages");

            migrationBuilder.AlterColumn<long>(
                name: "SenderId",
                table: "ConversationMessages",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ReceiverId",
                table: "ConversationMessages",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationMessages_ConversationMembers_ReceiverId",
                table: "ConversationMessages",
                column: "ReceiverId",
                principalTable: "ConversationMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationMessages_ConversationMembers_SenderId",
                table: "ConversationMessages",
                column: "SenderId",
                principalTable: "ConversationMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConversationMessages_ConversationMembers_ReceiverId",
                table: "ConversationMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ConversationMessages_ConversationMembers_SenderId",
                table: "ConversationMessages");

            migrationBuilder.AlterColumn<long>(
                name: "SenderId",
                table: "ConversationMessages",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ReceiverId",
                table: "ConversationMessages",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationMessages_ConversationMembers_ReceiverId",
                table: "ConversationMessages",
                column: "ReceiverId",
                principalTable: "ConversationMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationMessages_ConversationMembers_SenderId",
                table: "ConversationMessages",
                column: "SenderId",
                principalTable: "ConversationMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
