using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LenguajesIII.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LenguajesIII.Controllers
{
    [Route("api")]
    [ApiController]
    public class DetallesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DetallesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/GetTodosLosDetalles
        [HttpGet("GetTodosLosDetalles")]
        public async Task<IActionResult> GetTodosLosDetalles()
        {
            var result = await _context.Detalle.ToListAsync();

            if (result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/GetDetallePorId
        [HttpGet("GetDetallePorId")]
        public async Task<IActionResult> GetDetallePorId([FromBody] Detalles detalle)
        {
            if (string.IsNullOrEmpty(detalle.IdDetalle.ToString()))
            {
                return BadRequest("Request is incorrect");
            }

            var result = await _context.Detalle.FindAsync(detalle.IdDetalle);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("GetDetallePorIdPedido/{id}")]
        public ActionResult<Pedidos> GetDetallePorIdPedido(int id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest("Request is incorrect");
            }

            var result = _context.Detalle.Where(x => x.IdPedido == id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST: api/GuardarDetalle       
        [HttpPost("GuardarDetalle")]
        public async Task<ActionResult<Detalles>> GuardarDetalle([FromBody] Detalles detalle)
        {
            _context.Detalle.Add(detalle);
            await _context.SaveChangesAsync();
            return detalle;
        }        

        [HttpDelete("EliminarDetalle/{id}")]
        public async Task<ActionResult<int>> EliminarDetalle(int id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest("Request is incorrect");
            }

            var result = await _context.Detalle.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            _context.Detalle.Remove(result);
            await _context.SaveChangesAsync();
            return id;
        }
    }
}
