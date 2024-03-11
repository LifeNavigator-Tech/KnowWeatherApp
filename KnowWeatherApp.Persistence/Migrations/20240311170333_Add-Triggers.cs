using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowWeatherApp.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTriggers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TimeZoneOffset",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Triggers",
                columns: table => new
                {
                    TriggerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TriggerType = table.Column<int>(type: "int", nullable: false),
                    NotificationTypes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EqualityType = table.Column<int>(type: "int", nullable: false),
                    Threshold = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeToNotify = table.Column<TimeOnly>(type: "time", nullable: false),
                    TimeOfDayToCheck = table.Column<TimeOnly>(type: "time", nullable: false),
                    Field = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triggers", x => x.TriggerId);
                    table.ForeignKey(
                        name: "FK_Triggers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Triggers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Triggers_CityId",
                table: "Triggers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Triggers_UserId",
                table: "Triggers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Triggers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TimeZoneOffset",
                table: "AspNetUsers");
        }
    }
}
