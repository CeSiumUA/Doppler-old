using Microsoft.EntityFrameworkCore.Migrations;

namespace Doppler.REST.Migrations
{
    public partial class addedIsDialogflag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDialogue",
                table: "Conversations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDialogue",
                table: "Conversations");
        }
    }
}
