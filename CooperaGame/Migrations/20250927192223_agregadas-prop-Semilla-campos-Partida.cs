using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CooperaGame.Migrations
{
    /// <inheritdoc />
    public partial class agregadaspropSemillacamposPartida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Semilla",
                table: "Partidas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Semilla",
                table: "Partidas");
        }
    }
}
