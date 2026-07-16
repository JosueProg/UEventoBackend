using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UEventoBackend.Data;
using UEventoBackend.Models;

namespace UEventoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscripcionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InscripcionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Inscripciones/evento/5
        [HttpGet("evento/{eventoId}")]
        public async Task<ActionResult<IEnumerable<Inscripcion>>> GetInscripcionesPorEvento(int eventoId)
        {
            return await _context.Inscripciones
                .Where(i => i.EventoId == eventoId)
                .ToListAsync();
        }

        // POST: api/Inscripciones
        [HttpPost]
        public async Task<ActionResult<Inscripcion>> PostInscripcion(Inscripcion inscripcion)
        {
            inscripcion.Id = 0; // SQL Server generará automáticamente el ID identity
            _context.Inscripciones.Add(inscripcion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInscripcion), new { id = inscripcion.Id }, inscripcion);
        }

        // GET: api/Inscripciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inscripcion>> GetInscripcion(int id)
        {
            var inscripcion = await _context.Inscripciones.FindAsync(id);
            if (inscripcion == null) return NotFound();
            return inscripcion;
        }

        // PUT: api/Inscripciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInscripcion(int id, Inscripcion inscripcion)
        {
            if (id != inscripcion.Id) return BadRequest();

            _context.Entry(inscripcion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Inscripciones.Any(i => i.Id == id)) return NotFound();
                throw;
            }

            return Ok(inscripcion);
        }

        // DELETE: api/Inscripciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInscripcion(int id)
        {
            var inscripcion = await _context.Inscripciones.FindAsync(id);
            if (inscripcion == null) return NotFound();

            _context.Inscripciones.Remove(inscripcion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}