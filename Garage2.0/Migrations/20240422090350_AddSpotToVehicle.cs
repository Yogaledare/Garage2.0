using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage2._0.Migrations
{
    /// <inheritdoc />
    public partial class AddSpotToVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParkingSpotId",
                table: "Vehicles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ParkingSpot",
                columns: table => new
                {
                    ParkingSpotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Spot = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpot", x => x.ParkingSpotId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ParkingSpotId",
                table: "Vehicles",
                column: "ParkingSpotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_ParkingSpot_ParkingSpotId",
                table: "Vehicles",
                column: "ParkingSpotId",
                principalTable: "ParkingSpot",
                principalColumn: "ParkingSpotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_ParkingSpot_ParkingSpotId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "ParkingSpot");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_ParkingSpotId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ParkingSpotId",
                table: "Vehicles");
        }
    }
}
