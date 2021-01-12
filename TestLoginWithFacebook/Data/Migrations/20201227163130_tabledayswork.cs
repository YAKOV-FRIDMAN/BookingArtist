using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingArtistMvcCore.Data.Migrations
{
    public partial class tabledayswork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_DaysWork_FkIdArtis",
                table: "Artists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DaysWork",
                table: "DaysWork");

            migrationBuilder.RenameTable(
                name: "DaysWork",
                newName: "DaysWorks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DaysWorks",
                table: "DaysWorks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_DaysWorks_FkIdArtis",
                table: "Artists",
                column: "FkIdArtis",
                principalTable: "DaysWorks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_DaysWorks_FkIdArtis",
                table: "Artists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DaysWorks",
                table: "DaysWorks");

            migrationBuilder.RenameTable(
                name: "DaysWorks",
                newName: "DaysWork");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DaysWork",
                table: "DaysWork",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_DaysWork_FkIdArtis",
                table: "Artists",
                column: "FkIdArtis",
                principalTable: "DaysWork",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
