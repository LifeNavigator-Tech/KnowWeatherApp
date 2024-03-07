using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowWeatherApp.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustAppUserMore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "AppUserCity",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "AppUserCity",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUserCity_AppUserId",
                table: "AppUserCity",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserCity_CityId",
                table: "AppUserCity",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserCity_AspNetUsers_AppUserId",
                table: "AppUserCity",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserCity_Cities_CityId",
                table: "AppUserCity",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserCity_AspNetUsers_AppUserId",
                table: "AppUserCity");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserCity_Cities_CityId",
                table: "AppUserCity");

            migrationBuilder.DropIndex(
                name: "IX_AppUserCity_AppUserId",
                table: "AppUserCity");

            migrationBuilder.DropIndex(
                name: "IX_AppUserCity_CityId",
                table: "AppUserCity");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "AppUserCity");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "AppUserCity");
        }
    }
}
