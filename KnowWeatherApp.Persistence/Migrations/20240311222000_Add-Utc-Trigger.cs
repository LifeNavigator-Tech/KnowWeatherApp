using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowWeatherApp.API.Migrations
{
    /// <inheritdoc />
    public partial class AddUtcTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "TimeToNotifyUtc",
                table: "Triggers",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeToNotifyUtc",
                table: "Triggers");
        }
    }
}
