using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StatusAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Admin1Id",
                table: "VehicleOffers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Admin2Id",
                table: "VehicleOffers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "AdminStatus",
                table: "VehicleOffers",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "Admin1Id",
                table: "VehicleAds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Admin2Id",
                table: "VehicleAds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "VehicleAds",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "Admin1Id",
                table: "CargoOffers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Admin2Id",
                table: "CargoOffers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "AdminStatus",
                table: "CargoOffers",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "Admin1Id",
                table: "CargoAds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Admin2Id",
                table: "CargoAds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "CargoAds",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Admin1Id",
                table: "VehicleOffers");

            migrationBuilder.DropColumn(
                name: "Admin2Id",
                table: "VehicleOffers");

            migrationBuilder.DropColumn(
                name: "AdminStatus",
                table: "VehicleOffers");

            migrationBuilder.DropColumn(
                name: "Admin1Id",
                table: "VehicleAds");

            migrationBuilder.DropColumn(
                name: "Admin2Id",
                table: "VehicleAds");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "VehicleAds");

            migrationBuilder.DropColumn(
                name: "Admin1Id",
                table: "CargoOffers");

            migrationBuilder.DropColumn(
                name: "Admin2Id",
                table: "CargoOffers");

            migrationBuilder.DropColumn(
                name: "AdminStatus",
                table: "CargoOffers");

            migrationBuilder.DropColumn(
                name: "Admin1Id",
                table: "CargoAds");

            migrationBuilder.DropColumn(
                name: "Admin2Id",
                table: "CargoAds");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CargoAds");
        }
    }
}
