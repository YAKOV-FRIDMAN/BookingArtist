using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingArtistMvcCore.Data.Migrations
{
    public partial class orderTableAndFixRow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_DaysWorks_FkIdArtis",
                table: "Artists");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Artists_FkIdUser",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FkIdUser",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Artists_FkIdArtis",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "FkIdUser",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FkIdArtis",
                table: "Artists");

            migrationBuilder.AddColumn<int>(
                name: "FkIdArtis",
                table: "DaysWorks",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameClient = table.Column<string>(nullable: true),
                    PhoneNumberClient = table.Column<string>(nullable: true),
                    IdCity = table.Column<int>(nullable: false),
                    IfPaid = table.Column<bool>(nullable: false),
                    IfApprovedOrder = table.Column<bool>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    DateEvent = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    IdAtris = table.Column<int>(nullable: false),
                    FkIdArtis = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Artists_FkIdArtis",
                        column: x => x.FkIdArtis,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DaysWorks_FkIdArtis",
                table: "DaysWorks",
                column: "FkIdArtis",
                unique: true,
                filter: "[FkIdArtis] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FkIdArtis",
                table: "Orders",
                column: "FkIdArtis");

            migrationBuilder.AddForeignKey(
                name: "FK_DaysWorks_Artists_FkIdArtis",
                table: "DaysWorks",
                column: "FkIdArtis",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DaysWorks_Artists_FkIdArtis",
                table: "DaysWorks");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_DaysWorks_FkIdArtis",
                table: "DaysWorks");

            migrationBuilder.DropColumn(
                name: "FkIdArtis",
                table: "DaysWorks");

            migrationBuilder.AddColumn<int>(
                name: "FkIdUser",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FkIdArtis",
                table: "Artists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FkIdUser",
                table: "AspNetUsers",
                column: "FkIdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_FkIdArtis",
                table: "Artists",
                column: "FkIdArtis");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_DaysWorks_FkIdArtis",
                table: "Artists",
                column: "FkIdArtis",
                principalTable: "DaysWorks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Artists_FkIdUser",
                table: "AspNetUsers",
                column: "FkIdUser",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
