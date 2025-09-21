using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CooperaGame.Migrations
{
    /// <inheritdoc />
    public partial class AgregadoModelosJugadorRecoleccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CantComida",
                table: "Partidas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CantMadera",
                table: "Partidas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CantPiedra",
                table: "Partidas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Jugadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jugadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recolecciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Recurso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JugadorId = table.Column<int>(type: "int", nullable: false),
                    PartidaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recolecciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recolecciones_Jugadores_JugadorId",
                        column: x => x.JugadorId,
                        principalTable: "Jugadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recolecciones_Partidas_PartidaId",
                        column: x => x.PartidaId,
                        principalTable: "Partidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recolecciones_JugadorId",
                table: "Recolecciones",
                column: "JugadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Recolecciones_PartidaId",
                table: "Recolecciones",
                column: "PartidaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recolecciones");

            migrationBuilder.DropTable(
                name: "Jugadores");

            migrationBuilder.DropColumn(
                name: "CantComida",
                table: "Partidas");

            migrationBuilder.DropColumn(
                name: "CantMadera",
                table: "Partidas");

            migrationBuilder.DropColumn(
                name: "CantPiedra",
                table: "Partidas");
        }
    }
}
