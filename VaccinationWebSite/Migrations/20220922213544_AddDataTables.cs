using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VaccinationWebSite.Migrations
{
    public partial class AddDataTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Registers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Notified",
                table: "Registers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registers",
                table: "Registers",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Registers",
                table: "Registers");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Registers");

            migrationBuilder.DropColumn(
                name: "Notified",
                table: "Registers");
        }
    }
}
