using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UEventoBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estudiante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Calificacion = table.Column<int>(type: "int", nullable: false),
                    Anonimo = table.Column<bool>(type: "bit", nullable: false),
                    EventoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Ponentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Especialidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Institucion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Biografia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ponentes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recursos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Disponible = table.Column<bool>(type: "bit", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposEvento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposEvento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizadorId = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoEventoId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detalles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eventos_TiposEvento_TipoEventoId",
                        column: x => x.TipoEventoId,
                        principalTable: "TiposEvento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inscripciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventoId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoAsistencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiereCertificado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscripciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inscripciones_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Estudiantes",
                columns: new[] { "Id", "Email", "Nombre", "Password" },
                values: new object[,]
                {
                    { 1, "luis.gomez@ug.edu.ec", "Luis Gomez", "password123" },
                    { 2, "marta.sanchez@ug.edu.ec", "Marta Sanchez", "password123" }
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

            migrationBuilder.InsertData(
                table: "Ponentes",
                columns: new[] { "Id", "Biografia", "Correo", "Especialidad", "Institucion", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, "Experto en machine learning e infraestructuras de datos.", "alberto.ramos@ug.edu.ec", "Inteligencia Artificial", "Universidad de Guayaquil", "Dr. Alberto Ramos", "0991122334" },
                    { 2, "Auditora de seguridad informática y redes.", "sofia.castro@ug.edu.ec", "Ciberseguridad", "Universidad de Guayaquil", "Ing. Sofia Castro", "0988877665" }
                });

            migrationBuilder.InsertData(
                table: "Recursos",
                columns: new[] { "Id", "Cantidad", "Descripcion", "Disponible", "Nombre", "Tipo" },
                values: new object[,]
                {
                    { 1, 5, "Proyector 4K para auditorios principales.", true, "Proyector Epson", "Audiovisual" },
                    { 2, 10, "Micrófono de solapa y mano con base.", true, "Micrófono Inalámbrico", "Audio" }
                });

            migrationBuilder.InsertData(
                table: "TiposEvento",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Conferencia" },
                    { 2, "Taller" },
                    { 3, "Seminario" },
                    { 4, "Congreso" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Email", "Nombre", "Password", "Tipo" },
                values: new object[] { 1, "admin@ug.edu.ec", "Administrador General", "admin", "administrador" });

            migrationBuilder.InsertData(
                table: "Eventos",
                columns: new[] { "Id", "Detalles", "Fecha", "Imagen", "OrganizadorId", "TipoEventoId", "Titulo" },
                values: new object[,]
                {
                    { 1, "Taller práctico sobre la construcción de APIs RESTful.", "2026-08-15", "", 1, 2, "Introducción a .NET Core" },
                    { 2, "Encuentro anual con múltiples ponencias sobre tecnología.", "2026-09-10", "", 2, 4, "Congreso de Innovación Tecnológica" }
                });

            migrationBuilder.InsertData(
                table: "Inscripciones",
                columns: new[] { "Id", "Cedula", "EventoId", "Nombre", "RequiereCertificado", "TipoAsistencia" },
                values: new object[,]
                {
                    { 1, "0912345678", 1, "Luis Gomez", true, "Presencial" },
                    { 2, "0987654321", 1, "Marta Sanchez", false, "Virtual" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_TipoEventoId",
                table: "Eventos",
                column: "TipoEventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_EventoId",
                table: "Inscripciones",
                column: "EventoId");

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
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "Inscripciones");

            migrationBuilder.DropTable(
                name: "Organizadores");

            migrationBuilder.DropTable(
                name: "Ponentes");

            migrationBuilder.DropTable(
                name: "Recursos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "TiposEvento");
        }
    }
}
