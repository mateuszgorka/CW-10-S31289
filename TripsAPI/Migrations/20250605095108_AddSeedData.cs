using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TripsAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientTrips_Clients_IdClient",
                table: "ClientTrips");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientTrips_Trips_IdTrip",
                table: "ClientTrips");

            migrationBuilder.DropTable(
                name: "TripCountry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trips",
                table: "Trips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientTrips",
                table: "ClientTrips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "Trips",
                newName: "Trip");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Country");

            migrationBuilder.RenameTable(
                name: "ClientTrips",
                newName: "Client_Trip");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "Client");

            migrationBuilder.RenameIndex(
                name: "IX_ClientTrips_IdTrip",
                table: "Client_Trip",
                newName: "IX_Client_Trip_IdTrip");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trip",
                table: "Trip",
                column: "IdTrip");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "IdCountry");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Client_Trip",
                table: "Client_Trip",
                columns: new[] { "IdClient", "IdTrip" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Client",
                table: "Client",
                column: "IdClient");

            migrationBuilder.CreateTable(
                name: "Country_Trip",
                columns: table => new
                {
                    IdCountry = table.Column<int>(type: "int", nullable: false),
                    IdTrip = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country_Trip", x => new { x.IdCountry, x.IdTrip });
                    table.ForeignKey(
                        name: "FK_Country_Trip_Country_IdCountry",
                        column: x => x.IdCountry,
                        principalTable: "Country",
                        principalColumn: "IdCountry",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Country_Trip_Trip_IdTrip",
                        column: x => x.IdTrip,
                        principalTable: "Trip",
                        principalColumn: "IdTrip",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "IdClient", "Email", "FirstName", "LastName", "Pesel", "Telephone" },
                values: new object[,]
                {
                    { 1, "john@example.com", "John", "Smith", "90010112345", "123-456-789" },
                    { 2, "anna@example.com", "Anna", "Kowalska", "92020298765", "987-654-321" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "IdCountry", "Name" },
                values: new object[,]
                {
                    { 1, "Italy" },
                    { 2, "Germany" }
                });

            migrationBuilder.InsertData(
                table: "Trip",
                columns: new[] { "IdTrip", "DateFrom", "DateTo", "Description", "MaxPeople", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trip to Rome", 20, "Rome" },
                    { 2, new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trip to Berlin", 25, "Berlin" }
                });

            migrationBuilder.InsertData(
                table: "Client_Trip",
                columns: new[] { "IdClient", "IdTrip", "PaymentDate", "RegisteredAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, null, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Country_Trip",
                columns: new[] { "IdCountry", "IdTrip" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Country_Trip_IdTrip",
                table: "Country_Trip",
                column: "IdTrip");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Trip_Client_IdClient",
                table: "Client_Trip",
                column: "IdClient",
                principalTable: "Client",
                principalColumn: "IdClient",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Trip_Trip_IdTrip",
                table: "Client_Trip",
                column: "IdTrip",
                principalTable: "Trip",
                principalColumn: "IdTrip",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_Trip_Client_IdClient",
                table: "Client_Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_Client_Trip_Trip_IdTrip",
                table: "Client_Trip");

            migrationBuilder.DropTable(
                name: "Country_Trip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trip",
                table: "Trip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Client_Trip",
                table: "Client_Trip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Client",
                table: "Client");

            migrationBuilder.DeleteData(
                table: "Client_Trip",
                keyColumns: new[] { "IdClient", "IdTrip" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Client_Trip",
                keyColumns: new[] { "IdClient", "IdTrip" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "IdCountry",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "IdCountry",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Client",
                keyColumn: "IdClient",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Client",
                keyColumn: "IdClient",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Trip",
                keyColumn: "IdTrip",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Trip",
                keyColumn: "IdTrip",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "Trip",
                newName: "Trips");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "Countries");

            migrationBuilder.RenameTable(
                name: "Client_Trip",
                newName: "ClientTrips");

            migrationBuilder.RenameTable(
                name: "Client",
                newName: "Clients");

            migrationBuilder.RenameIndex(
                name: "IX_Client_Trip_IdTrip",
                table: "ClientTrips",
                newName: "IX_ClientTrips_IdTrip");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trips",
                table: "Trips",
                column: "IdTrip");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "IdCountry");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientTrips",
                table: "ClientTrips",
                columns: new[] { "IdClient", "IdTrip" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "IdClient");

            migrationBuilder.CreateTable(
                name: "TripCountry",
                columns: table => new
                {
                    CountriesIdCountry = table.Column<int>(type: "int", nullable: false),
                    TripsIdTrip = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripCountry", x => new { x.CountriesIdCountry, x.TripsIdTrip });
                    table.ForeignKey(
                        name: "FK_TripCountry_Countries_CountriesIdCountry",
                        column: x => x.CountriesIdCountry,
                        principalTable: "Countries",
                        principalColumn: "IdCountry",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripCountry_Trips_TripsIdTrip",
                        column: x => x.TripsIdTrip,
                        principalTable: "Trips",
                        principalColumn: "IdTrip",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TripCountry_TripsIdTrip",
                table: "TripCountry",
                column: "TripsIdTrip");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientTrips_Clients_IdClient",
                table: "ClientTrips",
                column: "IdClient",
                principalTable: "Clients",
                principalColumn: "IdClient",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientTrips_Trips_IdTrip",
                table: "ClientTrips",
                column: "IdTrip",
                principalTable: "Trips",
                principalColumn: "IdTrip",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
