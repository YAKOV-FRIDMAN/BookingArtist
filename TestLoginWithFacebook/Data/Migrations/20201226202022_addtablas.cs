using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingArtistMvcCore.Data.Migrations
{
    public partial class addtablas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FkIdUser",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistType = table.Column<int>(nullable: false),
                    IdUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Citys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CictyWorks",
                columns: table => new
                {
                    IdArtis = table.Column<int>(nullable: false),
                    IdCity = table.Column<int>(nullable: false),
                    ArtistsId = table.Column<int>(nullable: true),
                    CitysId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CictyWorks", x => new { x.IdArtis, x.IdCity });
                    table.ForeignKey(
                        name: "FK_CictyWorks_Artists_ArtistsId",
                        column: x => x.ArtistsId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CictyWorks_Citys_CitysId",
                        column: x => x.CitysId,
                        principalTable: "Citys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FkIdUser",
                table: "AspNetUsers",
                column: "FkIdUser");

            migrationBuilder.CreateIndex(
                name: "IX_CictyWorks_ArtistsId",
                table: "CictyWorks",
                column: "ArtistsId");

            migrationBuilder.CreateIndex(
                name: "IX_CictyWorks_CitysId",
                table: "CictyWorks",
                column: "CitysId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Artists_FkIdUser",
                table: "AspNetUsers",
                column: "FkIdUser",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Artists_FkIdUser",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CictyWorks");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "Citys");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FkIdUser",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FkIdUser",
                table: "AspNetUsers");
        }
    }
}
