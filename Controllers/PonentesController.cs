using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UEventoBackend.Data;
using UEventoBackend.Models;

namespace UEventoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PonentesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PonentesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Ponentes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PonenteModel>>> GetPonentes()
        {
            return await _context.Ponentes.ToListAsync();
        }

        // GET: api/Ponentes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PonenteModel>> GetPonenteModel(int id)
        {
            var ponenteModel = await _context.Ponentes.FindAsync(id);

            if (ponenteModel == null)
            {
                return NotFound();
            }

            return ponenteModel;
        }

        // PUT: api/Ponentes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPonenteModel(int id, PonenteModel ponenteModel)
        {
            if (id != ponenteModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(ponenteModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PonenteModelExists(id))
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

        // POST: api/Ponentes
        [HttpPost]
        public async Task<ActionResult<PonenteModel>> PostPonenteModel(PonenteModel ponenteModel)
        {
            _context.Ponentes.Add(ponenteModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPonenteModel", new { id = ponenteModel.Id }, ponenteModel);
        }

        // DELETE: api/Ponentes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePonenteModel(int id)
        {
            var ponenteModel = await _context.Ponentes.FindAsync(id);
            if (ponenteModel == null)
            {
                return NotFound();
            }

            _context.Ponentes.Remove(ponenteModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PonenteModelExists(int id)
        {
            return _context.Ponentes.Any(e => e.Id == id);
        }
    }
}
