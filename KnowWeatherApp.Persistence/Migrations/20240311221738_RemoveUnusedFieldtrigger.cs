using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowWeatherApp.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnusedFieldtrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TriggerType",
                table: "Triggers");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Created",
                table: "Triggers",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Triggers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Modified",
                table: "Triggers",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Triggers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Triggers");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Triggers");

            migrationBuilder.AddColumn<int>(
                name: "TriggerType",
                table: "Triggers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
