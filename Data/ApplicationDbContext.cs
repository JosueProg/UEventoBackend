using Microsoft.EntityFrameworkCore;
using UEventoBackend.Models;

namespace UEventoBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PonenteModel> Ponentes { get; set; }

        public DbSet<RecursoModel> Recursos { get; set; }

        public DbSet<ComentarioModel> Comentarios { get; set; }

        public DbSet<UsuarioModel> Usuarios { get; set; }
    }
}