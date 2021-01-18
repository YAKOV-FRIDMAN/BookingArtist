using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingArtistMvcCore.Data.Migrations
{
    public partial class clientandorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameClient",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PhoneNumberClient",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "FkIdClient",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdClient",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(nullable: true),
                    IdUser = table.Column<string>(nullable: true),
                    IdentityUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Client_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FkIdClient",
                table: "Orders",
                column: "FkIdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Client_IdentityUserId",
                table: "Client",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Client_FkIdClient",
                table: "Orders",
                column: "FkIdClient",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Client_FkIdClient",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Orders_FkIdClient",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FkIdClient",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IdClient",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "NameClient",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumberClient",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
