using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingArtistMvcCore.Data.Migrations
{
    public partial class priceforartisi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Artists",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Artists");
        }
    }
}
