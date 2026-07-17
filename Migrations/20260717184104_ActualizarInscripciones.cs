using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UEventoBackend.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarInscripciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RequiereCertificado",
                table: "Inscripciones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TipoAsistencia",
                table: "Inscripciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiereCertificado",
                table: "Inscripciones");

            migrationBuilder.DropColumn(
                name: "TipoAsistencia",
                table: "Inscripciones");
        }
    }
}
