using Microsoft.EntityFrameworkCore.Migrations;

namespace TestLoginWithFacebook.Data.Migrations
{
    public partial class ProfileArtistTableFix4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IdArtist",
                table: "ProfileArtists");

            migrationBuilder.AddColumn<int>(
                name: "FkIdArtis",
                table: "ProfileArtists",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdArtit",
                table: "ProfileArtists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileArtists_FkIdArtis",
                table: "ProfileArtists",
                column: "FkIdArtis",
                unique: true,
                filter: "[FkIdArtis] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileArtists_Artists_FkIdArtis",
                table: "ProfileArtists",
                column: "FkIdArtis",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileArtists_Artists_FkIdArtis",
                table: "ProfileArtists");

            migrationBuilder.DropIndex(
                name: "IX_ProfileArtists_FkIdArtis",
                table: "ProfileArtists");

            migrationBuilder.DropColumn(
                name: "FkIdArtis",
                table: "ProfileArtists");

            migrationBuilder.DropColumn(
                name: "IdArtit",
                table: "ProfileArtists");

            migrationBuilder.AddColumn<int>(
                name: "FkIdArtist",
                table: "ProfileArtists",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdArtist",
                table: "ProfileArtists",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
