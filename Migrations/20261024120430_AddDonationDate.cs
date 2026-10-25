
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodLoop.Migrations
{
    public partial class AddDonationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DonationDate",
                table: "Foods",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2026, 10, 24, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonationDate",
                table: "Foods");
        }
    }
}
