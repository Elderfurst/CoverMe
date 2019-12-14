using Microsoft.EntityFrameworkCore.Migrations;

namespace CoverMe.Migrations
{
    public partial class AddRainThreshold : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RainThreshold",
                table: "NotificationRequests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RainThreshold",
                table: "NotificationRequests");
        }
    }
}
