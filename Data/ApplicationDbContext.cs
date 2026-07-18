using Microsoft.EntityFrameworkCore;
using UEventoBackend.Models;

namespace UEventoBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<TipoEvento> TiposEvento { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<OrganizadorModel> Organizadores { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<PonenteModel> Ponentes { get; set; }
        public DbSet<RecursoModel> Recursos { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Evento>()
                .HasOne(e => e.TipoEvento)
                .WithMany(t => t.Eventos)
                .HasForeignKey(e => e.TipoEventoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Inscripcion>()
                .HasOne(i => i.Evento)
                .WithMany(e => e.Inscripciones)
                .HasForeignKey(i => i.EventoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrganizadorModel>(e =>
            {
                e.ToTable("Organizadores");
                e.HasKey(o => o.Id);
                e.Property(o => o.Nombre).IsRequired().HasMaxLength(120);
                e.Property(o => o.Email).IsRequired().HasMaxLength(150);
                e.Property(o => o.Password).IsRequired().HasMaxLength(200);
                e.Property(o => o.Facultad).HasMaxLength(100);
                e.Property(o => o.Cargo).HasMaxLength(100);
                e.Property(o => o.Telefono).HasMaxLength(20);
                e.HasIndex(o => o.Email).IsUnique();
            });

            // Datos semilla

            // Tipos de Evento
            modelBuilder.Entity<TipoEvento>().HasData(
                new TipoEvento { Id = 1, Nombre = "Conferencia" },
                new TipoEvento { Id = 2, Nombre = "Taller" },
                new TipoEvento { Id = 3, Nombre = "Seminario" },
                new TipoEvento { Id = 4, Nombre = "Congreso" }
            );

            // Organizadores
            modelBuilder.Entity<OrganizadorModel>().HasData(
                new OrganizadorModel { Id = 1, Nombre = "Carlos Mendoza", Email = "carlos@ug.edu.ec", Password = "1234", Facultad = "FCMF", Cargo = "Docente Titular", Telefono = "0991234567", Activo = true },
                new OrganizadorModel { Id = 2, Nombre = "Ana Torres", Email = "ana.torres@ug.edu.ec", Password = "1234", Facultad = "FING", Cargo = "Coordinadora de Club", Telefono = "0987654321", Activo = true },
                new OrganizadorModel { Id = 3, Nombre = "Roberto Silva", Email = "roberto.silva@ug.edu.ec", Password = "1234", Facultad = "FACS", Cargo = "Director Académico", Telefono = "0976543210", Activo = true },
                new OrganizadorModel { Id = 4, Nombre = "María López", Email = "maria.lopez@ug.edu.ec", Password = "1234", Facultad = "FCMF", Cargo = "Jefa de Departamento", Telefono = "0965432109", Activo = true },
                new OrganizadorModel { Id = 5, Nombre = "Juan Pérez", Email = "juan.perez@ug.edu.ec", Password = "1234", Facultad = "FING", Cargo = "Docente Investigador", Telefono = "0954321098", Activo = false }
            );

            // Estudiantes
            modelBuilder.Entity<Estudiante>().HasData(
                new Estudiante { Id = 1, Nombre = "Luis Gomez", Email = "luis.gomez@ug.edu.ec", Password = "password123" },
                new Estudiante { Id = 2, Nombre = "Marta Sanchez", Email = "marta.sanchez@ug.edu.ec", Password = "password123" }
            );

            // Ponentes
            modelBuilder.Entity<PonenteModel>().HasData(
                new PonenteModel { Id = 1, Nombre = "Dr. Alberto Ramos", Especialidad = "Inteligencia Artificial", Correo = "alberto.ramos@ug.edu.ec", Telefono = "0991122334", Institucion = "Universidad de Guayaquil", Biografia = "Experto en machine learning e infraestructuras de datos." },
                new PonenteModel { Id = 2, Nombre = "Ing. Sofia Castro", Especialidad = "Ciberseguridad", Correo = "sofia.castro@ug.edu.ec", Telefono = "0988877665", Institucion = "Universidad de Guayaquil", Biografia = "Auditora de seguridad informática y redes." }
            );

            // Recursos
            modelBuilder.Entity<RecursoModel>().HasData(
                new RecursoModel { Id = 1, Nombre = "Proyector Epson", Tipo = "Audiovisual", Cantidad = 5, Disponible = true, Descripcion = "Proyector 4K para auditorios principales." },
                new RecursoModel { Id = 2, Nombre = "Micrófono Inalámbrico", Tipo = "Audio", Cantidad = 10, Disponible = true, Descripcion = "Micrófono de solapa y mano con base." }
            );

            // Usuarios del sistema
            modelBuilder.Entity<UsuarioModel>().HasData(
                new UsuarioModel { Id = 1, Nombre = "Administrador General", Email = "admin@ug.edu.ec", Password = "admin", Tipo = "administrador" }
            );

            // Eventos
            modelBuilder.Entity<Evento>().HasData(
                new Evento { Id = 1, OrganizadorId = 1, Titulo = "Introducción a .NET Core", TipoEventoId = 2, Fecha = "2026-08-15", Detalles = "Taller práctico sobre la construcción de APIs RESTful.", Imagen = "" },
                new Evento { Id = 2, OrganizadorId = 2, Titulo = "Congreso de Innovación Tecnológica", TipoEventoId = 4, Fecha = "2026-09-10", Detalles = "Encuentro anual con múltiples ponencias sobre tecnología.", Imagen = "" }
            );

            // Inscripciones
            modelBuilder.Entity<Inscripcion>().HasData(
                new Inscripcion { Id = 1, EventoId = 1, Nombre = "Luis Gomez", Cedula = "0912345678", TipoAsistencia = "Presencial", RequiereCertificado = true },
                new Inscripcion { Id = 2, EventoId = 1, Nombre = "Marta Sanchez", Cedula = "0987654321", TipoAsistencia = "Virtual", RequiereCertificado = false }
            );

            // Comentarios
            modelBuilder.Entity<Comentario>().HasData(
                new Comentario { Id = 1, Estudiante = "Luis Gomez", Contenido = "El taller fue muy práctico y directo al grano, excelente.", Calificacion = 5, Anonimo = false, EventoId = 1 },
                new Comentario { Id = 2, Estudiante = "Anónimo", Contenido = "Me gustaría que los próximos eventos duren un poco más.", Calificacion = 4, Anonimo = true, EventoId = 2 }
            );
        }
    }
}