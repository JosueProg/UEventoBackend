using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UEventoBackend.Data;
using UEventoBackend.Models;

namespace UEventoBackend.Controllers
{
    // 1. Ruta explícita en minúsculas para igualar a Angular
    [Route("api/[controller]")]
    [ApiController]
    public class InscripcionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InscripcionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // NUEVO: Este método hará que la pantalla negra de error del navegador desaparezca
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inscripcion>>> GetInscripcionesRoot()
        {
            return await _context.Inscripciones.ToListAsync();
        }

        [HttpGet("evento/{eventoId}")]
        public async Task<ActionResult<IEnumerable<Inscripcion>>> GetInscripcionesPorEvento(int eventoId)
        {
            return await _context.Inscripciones
                .Where(i => i.EventoId == eventoId)
                .ToListAsync();
        }

        [HttpPost]
        // 2. Añadido [FromBody] para forzar el mapeo del JSON
        public async Task<ActionResult<Inscripcion>> PostInscripcion([FromBody] Inscripcion inscripcion)
        {
            inscripcion.Id = 0;
            _context.Inscripciones.Add(inscripcion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInscripcion), new { id = inscripcion.Id }, inscripcion);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Inscripcion>> GetInscripcion(int id)
        {
            var inscripcion = await _context.Inscripciones.FindAsync(id);
            if (inscripcion == null) return NotFound();
            return inscripcion;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInscripcion(int id, [FromBody] Inscripcion inscripcion)
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