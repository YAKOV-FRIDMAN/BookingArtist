using Microsoft.EntityFrameworkCore.Migrations;

namespace TestLoginWithFacebook.Data.Migrations
{
    public partial class ProfileArtistTableFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_ProfileArtists_ProfileArtistId",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Artists_ProfileArtistId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "ProfileArtistId",
                table: "Artists");

            migrationBuilder.AddColumn<int>(
                name: "ArtistId",
                table: "ProfileArtists",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileArtists_ArtistId",
                table: "ProfileArtists",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileArtists_Artists_ArtistId",
                table: "ProfileArtists",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileArtists_Artists_ArtistId",
                table: "ProfileArtists");

            migrationBuilder.DropIndex(
                name: "IX_ProfileArtists_ArtistId",
                table: "ProfileArtists");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "ProfileArtists");

            migrationBuilder.AddColumn<int>(
                name: "ProfileArtistId",
                table: "Artists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Artists_ProfileArtistId",
                table: "Artists",
                column: "ProfileArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_ProfileArtists_ProfileArtistId",
                table: "Artists",
                column: "ProfileArtistId",
                principalTable: "ProfileArtists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
