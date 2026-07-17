using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UEventoBackend.Dtos;
using UEventoBackend.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using UEventoBackend.Models;

namespace UEventoBackend.Controllers
{
    [Route("comentarios")] 
    [ApiController]
    public class ComentariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ComentariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /comentarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetComentarios() // 3. CORREGIDO: Cambiado ComentarioModel a Comentario
        {
            return await _context.Comentarios.ToListAsync();
        }

        // POST: /comentarios
        [HttpPost]
        public async Task<ActionResult<Comentario>> CrearComentario([FromBody] ComentarioDTO dto) 
        {
            
            var nuevoComentario = new Comentario
            {
                Estudiante = dto.Estudiante,
                Contenido = dto.Contenido,
                Calificacion = dto.Calificacion,
                Anonimo = dto.Anonimo,
                EventoId = dto.EventoId
            };

            _context.Comentarios.Add(nuevoComentario);
            await _context.SaveChangesAsync();

            // 6. CORREGIDO: Apunta al método real del controlador que es "GetComentarios"
            return CreatedAtAction(nameof(GetComentarios), new { id = nuevoComentario.Id }, nuevoComentario);
        }

        // PUT: /comentarios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarComentario(int id, [FromBody] ComentarioDTO dto)
        {
            var comentarioExistente = await _context.Comentarios.FindAsync(id);

            if (comentarioExistente == null)
            {
                return NotFound();
            }

            comentarioExistente.Estudiante = dto.Estudiante;
            comentarioExistente.Contenido = dto.Contenido;
            comentarioExistente.Calificacion = dto.Calificacion;
            comentarioExistente.Anonimo = dto.Anonimo;
            comentarioExistente.EventoId = dto.EventoId;

            _context.Entry(comentarioExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(comentarioExistente);
        }

        // DELETE: /comentarios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarComentario(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario == null)
            {
                return NotFound();
            }

            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
