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