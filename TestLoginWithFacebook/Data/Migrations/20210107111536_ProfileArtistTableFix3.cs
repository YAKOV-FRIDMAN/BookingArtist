using Microsoft.EntityFrameworkCore.Migrations;

namespace TestLoginWithFacebook.Data.Migrations
{
    public partial class ProfileArtistTableFix3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FkIdArtist",
                table: "ProfileArtists",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileArtists_FkIdArtist",
                table: "ProfileArtists",
                column: "FkIdArtist",
                unique: true,
                filter: "[FkIdArtist] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileArtists_Artists_FkIdArtist",
                table: "ProfileArtists",
                column: "FkIdArtist",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileArtists_Artists_FkIdArtist",
                table: "ProfileArtists");

            migrationBuilder.DropIndex(
                name: "IX_ProfileArtists_FkIdArtist",
                table: "ProfileArtists");

            migrationBuilder.DropColumn(
                name: "FkIdArtist",
                table: "ProfileArtists");

            migrationBuilder.AddColumn<int>(
                name: "ArtistId",
                table: "ProfileArtists",
                type: "int",
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
    }
}
