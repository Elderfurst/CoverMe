using Microsoft.EntityFrameworkCore.Migrations;

namespace CoverMe.Migrations
{
    public partial class AddLatAndLong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityId",
                table: "NotificationRequests");

            migrationBuilder.AddColumn<float>(
                name: "Latitude",
                table: "NotificationRequests",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Longitude",
                table: "NotificationRequests",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "NotificationRequests");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "NotificationRequests");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "NotificationRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
