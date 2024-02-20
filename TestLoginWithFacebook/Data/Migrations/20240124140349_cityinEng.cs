using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingArtistMvcCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class cityinEng : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CityEn",
                table: "Citys",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityEn",
                table: "Citys");
        }
    }
}
