using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UEventoBackend.Data;
using UEventoBackend.Models;

namespace UEventoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposEventoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TiposEventoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TiposEvento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoEvento>>> GetTiposEvento()
        {
            return await _context.TiposEvento.ToListAsync();
        }

        // GET: api/TiposEvento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoEvento>> GetTipoEvento(int id)
        {
            var tipoEvento = await _context.TiposEvento.FindAsync(id);
            if (tipoEvento == null) return NotFound();
            return tipoEvento;
        }
    }
}