using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBookingAPI.Migrations
{
    /// <inheritdoc />
    public partial class Adduniquekey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AirlineCode",
                table: "Airlines",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Airlines_AirlineCode",
                table: "Airlines",
                column: "AirlineCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Airlines_AirlineCode",
                table: "Airlines");

            migrationBuilder.AlterColumn<string>(
                name: "AirlineCode",
                table: "Airlines",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
