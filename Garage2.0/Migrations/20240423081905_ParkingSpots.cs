using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage2._0.Migrations
{
    /// <inheritdoc />
    public partial class ParkingSpots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_ParkingSpot_ParkingSpotId",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParkingSpot",
                table: "ParkingSpot");

            migrationBuilder.RenameTable(
                name: "ParkingSpot",
                newName: "ParkingSpots");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParkingSpots",
                table: "ParkingSpots",
                column: "ParkingSpotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_ParkingSpots_ParkingSpotId",
                table: "Vehicles",
                column: "ParkingSpotId",
                principalTable: "ParkingSpots",
                principalColumn: "ParkingSpotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_ParkingSpots_ParkingSpotId",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParkingSpots",
                table: "ParkingSpots");

            migrationBuilder.RenameTable(
                name: "ParkingSpots",
                newName: "ParkingSpot");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParkingSpot",
                table: "ParkingSpot",
                column: "ParkingSpotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_ParkingSpot_ParkingSpotId",
                table: "Vehicles",
                column: "ParkingSpotId",
                principalTable: "ParkingSpot",
                principalColumn: "ParkingSpotId");
        }
    }
}
