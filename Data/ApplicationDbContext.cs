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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación Evento -> TipoEvento
            modelBuilder.Entity<Evento>()
                .HasOne(e => e.TipoEvento)
                .WithMany(t => t.Eventos)
                .HasForeignKey(e => e.TipoEventoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Inscripcion -> Evento
            modelBuilder.Entity<Inscripcion>()
                .HasOne(i => i.Evento)
                .WithMany(e => e.Inscripciones)
                .HasForeignKey(i => i.EventoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Data para TipoEvento
            modelBuilder.Entity<TipoEvento>().HasData(
                new TipoEvento { Id = 1, Nombre = "Conferencia" },
                new TipoEvento { Id = 2, Nombre = "Taller" },
                new TipoEvento { Id = 3, Nombre = "Seminario" },
                new TipoEvento { Id = 4, Nombre = "Congreso" }
            );

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

                // El correo es la credencial de acceso: debe ser único.
                e.HasIndex(o => o.Email).IsUnique();
            });

            // Seed Data para Organizadores
            modelBuilder.Entity<OrganizadorModel>().HasData(
                new OrganizadorModel { Id = 1, Nombre = "Carlos Mendoza", Email = "carlos@ug.edu.ec", Password = "1234", Facultad = "FCMF", Cargo = "Docente Titular", Telefono = "0991234567", Activo = true },
                new OrganizadorModel { Id = 2, Nombre = "Ana Torres", Email = "ana.torres@ug.edu.ec", Password = "1234", Facultad = "FING", Cargo = "Coordinadora de Club", Telefono = "0987654321", Activo = true },
                new OrganizadorModel { Id = 3, Nombre = "Roberto Silva", Email = "roberto.silva@ug.edu.ec", Password = "1234", Facultad = "FACS", Cargo = "Director Académico", Telefono = "0976543210", Activo = true },
                new OrganizadorModel { Id = 4, Nombre = "María López", Email = "maria.lopez@ug.edu.ec", Password = "1234", Facultad = "FCMF", Cargo = "Jefa de Departamento", Telefono = "0965432109", Activo = true },
                new OrganizadorModel { Id = 5, Nombre = "Juan Pérez", Email = "juan.perez@ug.edu.ec", Password = "1234", Facultad = "FING", Cargo = "Docente Investigador", Telefono = "0954321098", Activo = false }
            );
        }
    }
}