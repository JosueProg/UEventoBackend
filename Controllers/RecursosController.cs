using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UEventoBackend.Data;
using UEventoBackend.Models;


namespace UEventoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecursosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RecursosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Recursos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecursoModel>>> GetRecursos()
        {
            return await _context.Recursos.ToListAsync();
        }

        // GET: api/Recursos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecursoModel>> GetRecursoModel(int id)
        {
            var recursoModel = await _context.Recursos.FindAsync(id);

            if (recursoModel == null)
            {
                return NotFound();
            }

            return recursoModel;
        }

        // PUT: api/Recursos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecursoModel(int id, RecursoModel recursoModel)
        {
            if (id != recursoModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(recursoModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecursoModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Recursos
        [HttpPost]
        public async Task<ActionResult<RecursoModel>> PostRecursoModel(RecursoModel recursoModel)
        {
            _context.Recursos.Add(recursoModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecursoModel", new { id = recursoModel.Id }, recursoModel);
        }

        // DELETE: api/Recursos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecursoModel(int id)
        {
            var recursoModel = await _context.Recursos.FindAsync(id);
            if (recursoModel == null)
            {
                return NotFound();
            }

            _context.Recursos.Remove(recursoModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecursoModelExists(int id)
        {
            return _context.Recursos.Any(e => e.Id == id);
        }
    }
}
