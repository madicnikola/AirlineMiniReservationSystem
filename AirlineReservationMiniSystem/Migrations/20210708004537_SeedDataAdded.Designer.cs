// <auto-generated />
using System;
using AirlineReservationMiniSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AirlineReservationMiniSystem.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210708004537_SeedDataAdded")]
    partial class SeedDataAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AirlineReservationMiniSystem.Models.Flight", b =>
                {
                    b.Property<int>("FlightId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DepartureCity")
                        .HasColumnType("int");

                    b.Property<DateTime>("DepartureDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DestinationCity")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfFreeSeats")
                        .HasColumnType("int");

                    b.Property<int>("TotalNumberOfSeats")
                        .HasColumnType("int");

                    b.HasKey("FlightId");

                    b.ToTable("Flights");

                    b.HasData(
                        new
                        {
                            FlightId = 1,
                            DepartureCity = 0,
                            DepartureDateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DestinationCity = 1,
                            NumberOfFreeSeats = 100,
                            TotalNumberOfSeats = 100
                        },
                        new
                        {
                            FlightId = 2,
                            DepartureCity = 1,
                            DepartureDateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DestinationCity = 3,
                            NumberOfFreeSeats = 100,
                            TotalNumberOfSeats = 100
                        },
                        new
                        {
                            FlightId = 3,
                            DepartureCity = 0,
                            DepartureDateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DestinationCity = 2,
                            NumberOfFreeSeats = 100,
                            TotalNumberOfSeats = 100
                        });
                });

            modelBuilder.Entity("AirlineReservationMiniSystem.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfReservation")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FlightId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfReservedSeats")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReservationId");

                    b.HasIndex("FlightId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("AirlineReservationMiniSystem.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("UsersDbSet");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "Nikola",
                            Name = "Nikola",
                            Password = "Nikola",
                            Role = 1,
                            Surname = "Nikola"
                        });
                });

            modelBuilder.Entity("AirlineReservationMiniSystem.Models.Reservation", b =>
                {
                    b.HasOne("AirlineReservationMiniSystem.Models.Flight", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightId");

                    b.HasOne("AirlineReservationMiniSystem.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
