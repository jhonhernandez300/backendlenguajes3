using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LenguajesIII.Models;
using System.Linq;
using System.Threading.Tasks;
using System;

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
            try
            {                
                var result = await _context.Detalle.ToListAsync();
                
                if (result.Count == 0)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Internal Server Error");
            }                        
        }

        // GET: api/GetDetallePorId
        [HttpGet("GetDetallePorId/{id}")]
        public async Task<IActionResult> GetDetallePorId(int id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest("Request is incorrect");
            }
            try
            {
                var result = await _context.Detalle.FindAsync(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("GetDetallesPorIdPedido/{id}")]
        public ActionResult<Pedidos> GetDetallesPorIdPedido(int id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest("Request is incorrect");
            }

            try
            {
                var result = _context.Detalle.Where(x => x.IdPedido == id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Internal Server Error");
            }
        }

      

        // POST: api/GuardarDetalle       
        [HttpPost("GuardarDetalle")]
        public async Task<ActionResult<Detalles>> GuardarDetalle([FromBody] Detalles detalle)
        {
            if (string.IsNullOrEmpty(detalle.IdDetalle.ToString()))
            {
                return BadRequest("Request is incorrect");
            }
            
            try
            {
                _context.Detalle.Add(detalle);
                await _context.SaveChangesAsync();
                return detalle;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Internal Server Error");
            }           
        }        

        [HttpDelete("EliminarDetalle/{id}")]
        public async Task<ActionResult<int>> EliminarDetalle(int id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest("Request is incorrect");
            }

            try
            {
                var result = await _context.Detalle.FindAsync(id);

                if (result == null)
                {
                    return NotFound();
                }

                _context.Detalle.Remove(result);
                await _context.SaveChangesAsync();
                return id;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Internal Server Error");
            }
        }        
    }
}
