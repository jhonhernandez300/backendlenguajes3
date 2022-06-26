using Microsoft.AspNetCore.Mvc;
using LenguajesIII.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LenguajesIII.Controllers
{
    [Route("api")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PedidoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/GetTodosLosPedidos
        [HttpGet("GetTodosLosPedidos")]
        public async Task<IActionResult> GetTodosLosPedidos()
        {
            var result = await _context.Pedido.ToListAsync();

            if (result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }
       
        [HttpGet("GetPedidoPorId/{id}")]
        public async Task<ActionResult<int>> GetPedidoPorId(int id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest("Request is incorrect");
            }

            var result = await _context.Pedido.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST: api/GuardarPedido       
        [HttpPost("GuardarPedido")]
        public async Task<ActionResult<Pedidos>> GuardarPedido([FromBody] Pedidos pedido)
        {
            _context.Pedido.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        
        [HttpPatch("ActualizarPedido")]         
        public async Task<ActionResult<Pedidos>> ActualizarPedido([FromBody] Pedidos pedido)
        {
            if (string.IsNullOrEmpty(pedido.IdPedido.ToString()))
            {
                return BadRequest("Request is incorrect");
            }

            var result = await _context.Pedido.FindAsync(pedido.IdPedido);

            if (result == null)
            {
                return NotFound();
            }
            else {
                try
                {
                    result.Detalle = pedido.Detalle;
                    result.TelefonoCliente = pedido.TelefonoCliente;
                    result.DireccionCliente = pedido.DireccionCliente;
                    result.NombreCliente = pedido.NombreCliente;
                    
                    _context.Pedido.Update(result);
                    await _context.SaveChangesAsync();
                    return Ok(result);
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e);
                    return StatusCode(500, "Internal Server Error");
                }
                
            }
        }

        [HttpDelete("EliminarPedido/{id}")]
        public async Task<ActionResult<int>> EliminarPedido(int id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest("Request is incorrect");
            }

            var result = await _context.Pedido.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            _context.Pedido.Remove(result);
            await _context.SaveChangesAsync();
            return id;
        }
    }
}
