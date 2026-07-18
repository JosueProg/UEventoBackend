using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UEventoBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitOrganizadores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Facultad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizadores", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Organizadores",
                columns: new[] { "Id", "Activo", "Cargo", "Email", "Facultad", "Nombre", "Password", "Telefono" },
                values: new object[,]
                {
                    { 1, true, "Docente Titular", "carlos@ug.edu.ec", "FCMF", "Carlos Mendoza", "1234", "0991234567" },
                    { 2, true, "Coordinadora de Club", "ana.torres@ug.edu.ec", "FING", "Ana Torres", "1234", "0987654321" },
                    { 3, true, "Director Académico", "roberto.silva@ug.edu.ec", "FACS", "Roberto Silva", "1234", "0976543210" },
                    { 4, true, "Jefa de Departamento", "maria.lopez@ug.edu.ec", "FCMF", "María López", "1234", "0965432109" },
                    { 5, false, "Docente Investigador", "juan.perez@ug.edu.ec", "FING", "Juan Pérez", "1234", "0954321098" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Organizadores_Email",
                table: "Organizadores",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Organizadores");
        }
    }
}
