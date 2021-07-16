using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AirlineReservationMiniSystem.Migrations
{
    public partial class SeedDataAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureCity", "DepartureDateTime", "DestinationCity", "NumberOfFreeSeats", "TotalNumberOfSeats" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 100, 100 },
                    { 2, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 100, 100 },
                    { 3, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 100, 100 }
                });

            migrationBuilder.InsertData(
                table: "UsersDbSet",
                columns: new[] { "UserId", "Email", "Name", "Password", "Role", "Surname" },
                values: new object[] { 1, "Nikola", "Nikola", "Nikola", 1, "Nikola" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UsersDbSet",
                keyColumn: "UserId",
                keyValue: 1);
        }
    }
}
