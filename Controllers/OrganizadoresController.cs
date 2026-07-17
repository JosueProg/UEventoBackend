using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UEventoBackend.Data;
using UEventoBackend.Dtos;
using UEventoBackend.Models;

namespace UEventoBackend.Controllers
{
    /// <summary>
    /// API del módulo de Organizadores: CRUD, login y dashboard.
    /// [AllowAnonymous] evita que la política global de autenticación de
    /// Windows (Negotiate) bloquee las llamadas desde el frontend Angular.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class OrganizadoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrganizadoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/organizadores?buscar=ana&soloActivos=true
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizadorModel>>> GetOrganizadores(
            [FromQuery] string? buscar = null,
            [FromQuery] bool? soloActivos = null)
        {
            IQueryable<OrganizadorModel> query = _context.Organizadores.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                var q = buscar.Trim().ToLower();
                query = query.Where(o =>
                    o.Nombre.ToLower().Contains(q) ||
                    o.Email.ToLower().Contains(q) ||
                    o.Facultad.ToLower().Contains(q));
            }

            if (soloActivos == true)
            {
                query = query.Where(o => o.Activo);
            }

            var lista = await query.OrderBy(o => o.Id).ToListAsync();
            return Ok(lista);
        }

        // GET: api/organizadores/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrganizadorModel>> GetOrganizador(int id)
        {
            var organizador = await _context.Organizadores.FindAsync(id);
            if (organizador is null)
                return NotFound(new { mensaje = $"No existe el organizador con id {id}." });

            return Ok(organizador);
        }

        // POST: api/organizadores
        [HttpPost]
        public async Task<ActionResult<OrganizadorModel>> CrearOrganizador([FromBody] OrganizadorModel organizador)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var emailEnUso = await _context.Organizadores
                .AnyAsync(o => o.Email.ToLower() == organizador.Email.ToLower());
            if (emailEnUso)
                return Conflict(new { mensaje = "El correo ya está registrado." });

            organizador.Id = 0; // la base de datos asigna el id
            _context.Organizadores.Add(organizador);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrganizador), new { id = organizador.Id }, organizador);
        }

        // PUT: api/organizadores/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> ActualizarOrganizador(int id, [FromBody] OrganizadorModel datos)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var organizador = await _context.Organizadores.FindAsync(id);
            if (organizador is null)
                return NotFound(new { mensaje = $"No existe el organizador con id {id}." });

            var emailEnUsoPorOtro = await _context.Organizadores
                .AnyAsync(o => o.Id != id && o.Email.ToLower() == datos.Email.ToLower());
            if (emailEnUsoPorOtro)
                return Conflict(new { mensaje = "El correo ya está registrado por otro organizador." });

            organizador.Nombre = datos.Nombre;
            organizador.Email = datos.Email;
            organizador.Password = datos.Password;
            organizador.Facultad = datos.Facultad;
            organizador.Cargo = datos.Cargo;
            organizador.Telefono = datos.Telefono;
            organizador.Activo = datos.Activo;

            await _context.SaveChangesAsync();
            return Ok(organizador);
        }

        // DELETE: api/organizadores/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> EliminarOrganizador(int id)
        {
            var organizador = await _context.Organizadores.FindAsync(id);
            if (organizador is null)
                return NotFound(new { mensaje = $"No existe el organizador con id {id}." });

            _context.Organizadores.Remove(organizador);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/organizadores/login
        [HttpPost("login")]
        public async Task<ActionResult<OrganizadorModel>> Login([FromBody] LoginRequest credenciales)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var email = credenciales.Email.Trim().ToLower();

            var organizador = await _context.Organizadores
                .FirstOrDefaultAsync(o =>
                    o.Email.ToLower() == email &&
                    o.Password == credenciales.Password &&
                    o.Activo);

            if (organizador is null)
                return Unauthorized(new { mensaje = "Credenciales inválidas o cuenta inactiva." });

            return Ok(organizador);
        }

        // GET: api/organizadores/dashboard
        [HttpGet("dashboard")]
        public async Task<ActionResult<DashboardResponse>> GetDashboard()
        {
            var organizadores = await _context.Organizadores.AsNoTracking().ToListAsync();

            var dashboard = new DashboardResponse
            {
                Total = organizadores.Count,
                Activos = organizadores.Count(o => o.Activo),
                Inactivos = organizadores.Count(o => !o.Activo),
                PorFacultad = organizadores
                    .GroupBy(o => o.Facultad)
                    .Select(g => new ConteoPorGrupo(g.Key, g.Count()))
                    .OrderByDescending(c => c.Cantidad)
                    .ToList(),
                PorCargo = organizadores
                    .GroupBy(o => o.Cargo)
                    .Select(g => new ConteoPorGrupo(g.Key, g.Count()))
                    .OrderByDescending(c => c.Cantidad)
                    .ToList()
            };

            return Ok(dashboard);
        }
    }
}
