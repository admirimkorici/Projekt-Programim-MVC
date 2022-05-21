using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekt_Programim_MVC.Data.Migrations
{
    public partial class FirsMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mesazhe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subjekti = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mesazhi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Koha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Statusi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesazhe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tipi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ikona = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Makina",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modeli = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pershkrimi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vit_Prodhimi = table.Column<int>(type: "int", nullable: false),
                    ERezervuar = table.Column<bool>(type: "bit", nullable: false),
                    Kosto1Dite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IMG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipiID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makina", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Makina_Tipi_TipiID",
                        column: x => x.TipiID,
                        principalTable: "Tipi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rezervimet",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date_Rezervimi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date_kthimi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pagesa_totale = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MakinatID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervimet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rezervimet_Makina_MakinatID",
                        column: x => x.MakinatID,
                        principalTable: "Makina",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Makina_TipiID",
                table: "Makina",
                column: "TipiID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervimet_MakinatID",
                table: "Rezervimet",
                column: "MakinatID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mesazhe");

            migrationBuilder.DropTable(
                name: "Rezervimet");

            migrationBuilder.DropTable(
                name: "Makina");

            migrationBuilder.DropTable(
                name: "Tipi");
        }
    }
}
