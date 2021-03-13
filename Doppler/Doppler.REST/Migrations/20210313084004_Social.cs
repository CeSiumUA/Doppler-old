using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Doppler.REST.Migrations
{
    public partial class Social : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BLOBId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Blobs_BLOBId",
                        column: x => x.BLOBId,
                        principalTable: "Blobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversations_Files_IconId",
                        column: x => x.IconId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConversationMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConversationId = table.Column<int>(type: "int", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversationMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConversationMembers_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConversationMembers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConversationMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: true),
                    ReceiverId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    SentOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversationMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConversationMessages_ConversationMembers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "ConversationMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConversationMessages_ConversationMembers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "ConversationMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConversationMessageContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversationMessageContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConversationMessageContents_ConversationMessages_Id",
                        column: x => x.Id,
                        principalTable: "ConversationMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConversationMessageViewers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: true),
                    MessageId = table.Column<int>(type: "int", nullable: true),
                    ViewedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Viewed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversationMessageViewers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConversationMessageViewers_ConversationMembers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "ConversationMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConversationMessageViewers_ConversationMessages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "ConversationMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConversationMessageMediaContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConversationMessageContentId = table.Column<int>(type: "int", nullable: true),
                    ContentType = table.Column<int>(type: "int", nullable: false),
                    DataId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversationMessageMediaContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConversationMessageMediaContents_ConversationMessageContents_ConversationMessageContentId",
                        column: x => x.ConversationMessageContentId,
                        principalTable: "ConversationMessageContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConversationMessageMediaContents_Files_DataId",
                        column: x => x.DataId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConversationMembers_ConversationId",
                table: "ConversationMembers",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationMembers_UserId",
                table: "ConversationMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationMessageMediaContents_ConversationMessageContentId",
                table: "ConversationMessageMediaContents",
                column: "ConversationMessageContentId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationMessageMediaContents_DataId",
                table: "ConversationMessageMediaContents",
                column: "DataId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationMessages_ReceiverId",
                table: "ConversationMessages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationMessages_SenderId",
                table: "ConversationMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationMessageViewers_MemberId",
                table: "ConversationMessageViewers",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationMessageViewers_MessageId",
                table: "ConversationMessageViewers",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_IconId",
                table: "Conversations",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_BLOBId",
                table: "Files",
                column: "BLOBId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConversationMessageMediaContents");

            migrationBuilder.DropTable(
                name: "ConversationMessageViewers");

            migrationBuilder.DropTable(
                name: "ConversationMessageContents");

            migrationBuilder.DropTable(
                name: "ConversationMessages");

            migrationBuilder.DropTable(
                name: "ConversationMembers");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Blobs");
        }
    }
}
