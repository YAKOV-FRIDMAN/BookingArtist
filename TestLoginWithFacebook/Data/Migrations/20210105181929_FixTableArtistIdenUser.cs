using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingArtistMvcCore.Data.Migrations
{
    public partial class FixTableArtistIdenUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Artists",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Artists_IdentityUserId",
                table: "Artists",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_AspNetUsers_IdentityUserId",
                table: "Artists",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_AspNetUsers_IdentityUserId",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Artists_IdentityUserId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Artists");
        }
    }
}
