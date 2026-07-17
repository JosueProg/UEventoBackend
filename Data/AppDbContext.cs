using Microsoft.EntityFrameworkCore;
using UEventoBackend.Models;

namespace UEventoBackend.Data
{
    /// <summary>
    /// Contexto de base de datos compartido de la aplicación.
    /// Cada integrante del equipo agrega aquí el DbSet de su módulo.
    /// Por ahora contiene el módulo de Organizadores.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // ----- Módulo Organizadores (nuestro) -----
        public DbSet<OrganizadorModel> Organizadores => Set<OrganizadorModel>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            // Datos semilla: coinciden con public/json/organizadores.json del frontend.
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
