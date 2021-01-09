using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestLoginWithFacebook.Data.Migrations
{
    public partial class ProfileArtistTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageProfile",
                table: "Artists");

            migrationBuilder.AddColumn<int>(
                name: "ProfileArtistId",
                table: "Artists",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProfileArtists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageProfile = table.Column<byte[]>(nullable: true),
                    About = table.Column<string>(nullable: true),
                    IdArtist = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileArtists", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_ProfileArtists_ProfileArtistId",
                table: "Artists");

            migrationBuilder.DropTable(
                name: "ProfileArtists");

            migrationBuilder.DropIndex(
                name: "IX_Artists_ProfileArtistId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "ProfileArtistId",
                table: "Artists");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageProfile",
                table: "Artists",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
