﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowWeatherApp.API.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdateCreatedToReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "WeatherReports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "WeatherReports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "WeatherReports");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "WeatherReports");
        }
    }
}
