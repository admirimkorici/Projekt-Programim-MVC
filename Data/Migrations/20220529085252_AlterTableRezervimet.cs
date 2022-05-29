using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekt_Programim_MVC.Data.Migrations
{
    public partial class AlterTableRezervimet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adresa",
                table: "Rezervimet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NumerTelefoni",
                table: "Rezervimet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adresa",
                table: "Rezervimet");

            migrationBuilder.DropColumn(
                name: "NumerTelefoni",
                table: "Rezervimet");
        }
    }
}
