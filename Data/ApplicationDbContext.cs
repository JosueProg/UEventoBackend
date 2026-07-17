using Microsoft.EntityFrameworkCore;
using UEventoBackend.Models;

namespace UEventoBackend.Data
{

    public class ApplicationDbContext : DbContext
    {
              public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Comentario> Comentarios { get; set; }
    }
}
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<TipoEvento> TiposEvento { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }

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
        }
    }
}
