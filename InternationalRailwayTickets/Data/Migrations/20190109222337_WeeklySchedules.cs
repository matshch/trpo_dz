using Microsoft.EntityFrameworkCore.Migrations;

namespace InternationalRailwayTickets.Data.Migrations
{
    public partial class WeeklySchedules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DaysOfWeekJson",
                table: "Schedule",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysOfWeekJson",
                table: "Schedule");
        }
    }
}
