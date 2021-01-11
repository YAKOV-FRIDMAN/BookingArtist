using Microsoft.EntityFrameworkCore.Migrations;

namespace TestLoginWithFacebook.Data.Migrations
{
    public partial class addFullNameToProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "ProfileArtists",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "ProfileArtists");
        }
    }
}
