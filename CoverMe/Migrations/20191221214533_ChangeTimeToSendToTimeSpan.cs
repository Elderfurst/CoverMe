using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoverMe.Migrations
{
    public partial class ChangeTimeToSendToTimeSpan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timezone",
                table: "NotificationRequests");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "TimeToSend",
                table: "NotificationRequests",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeToSend",
                table: "NotificationRequests",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AddColumn<string>(
                name: "Timezone",
                table: "NotificationRequests",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
