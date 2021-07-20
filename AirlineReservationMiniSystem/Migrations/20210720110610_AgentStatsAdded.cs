using Microsoft.EntityFrameworkCore.Migrations;

namespace AirlineReservationMiniSystem.Migrations
{
    public partial class AgentStatsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AgentId",
                table: "Reservations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Reservations");

            migrationBuilder.CreateIndex(
                name: "IX_AgentReservations_ReservationId1",
                table: "AgentReservations",
                column: "ReservationId1");
        }
    }
}
