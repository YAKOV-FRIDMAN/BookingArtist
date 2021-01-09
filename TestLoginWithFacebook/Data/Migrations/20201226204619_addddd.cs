using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestLoginWithFacebook.Data.Migrations
{
    public partial class addddd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventType",
                table: "Artists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FkIdArtis",
                table: "Artists",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageProfile",
                table: "Artists",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DaysWork",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sunday = table.Column<bool>(nullable: false),
                    Monday = table.Column<bool>(nullable: false),
                    Tuesday = table.Column<bool>(nullable: false),
                    Wednesday = table.Column<bool>(nullable: false),
                    Thursday = table.Column<bool>(nullable: false),
                    Friday = table.Column<bool>(nullable: false),
                    IdArtit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaysWork", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artists_FkIdArtis",
                table: "Artists",
                column: "FkIdArtis");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_DaysWork_FkIdArtis",
                table: "Artists",
                column: "FkIdArtis",
                principalTable: "DaysWork",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_DaysWork_FkIdArtis",
                table: "Artists");

            migrationBuilder.DropTable(
                name: "DaysWork");

            migrationBuilder.DropIndex(
                name: "IX_Artists_FkIdArtis",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "EventType",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "FkIdArtis",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "ImageProfile",
                table: "Artists");
        }
    }
}
