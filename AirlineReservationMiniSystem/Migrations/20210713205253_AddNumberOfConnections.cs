using Microsoft.EntityFrameworkCore.Migrations;

namespace AirlineReservationMiniSystem.Migrations
{
    public partial class AddNumberOfConnections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfConnections",
                table: "Flights",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfConnections",
                table: "Flights");
        }
    }
}
