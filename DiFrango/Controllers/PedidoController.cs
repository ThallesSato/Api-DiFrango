using DiFrango.Database;
using DiFrango.Dtos;
using DiFrango.Models;
using DiFrango.Services;
using DiFrango.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiFrango.Controllers
{
    [Route("api/Pedidos")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_pedidoService.GetAllPedidos());
        }
        [HttpGet("hoje")]
        public IActionResult GetAllHoje()
        {
            return Ok(_pedidoService.GetAllByDatePedidos(DateTime.Now));
        }
        [HttpGet("hoje/assado")]
        public IActionResult GetAllHojeEAssado()
        {
            return Ok(_pedidoService.GetAllByDateAndCategoriaPedidos(DateTime.Now,1));
        }
        [HttpGet("hoje/assado_outro")]
        public IActionResult GetAllHojeEAssadoEOutro()
        {
            return Ok(_pedidoService.GetAllByDateAndCategoriaPedidos(DateTime.Now,1,3));
        }
        [HttpGet("hoje/frito")]
        public IActionResult GetAllHojeEFrito()
        {
            return Ok(_pedidoService.GetAllByDateAndCategoriaPedidos(DateTime.Now,2));
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_pedidoService.GetByIdPedido(id));
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("cliente/{id}")]
        public IActionResult GetByCliente(int id)
        {
            try
            {
                return Ok(_pedidoService.GetByClientePedido(id));
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
        
        
        [HttpPost]
        public IActionResult Post(PedidoDto pedidoDto)
        {
            try
            {
                var pedido = _pedidoService.DtoToPedido(pedidoDto);
                var novoPedido = _pedidoService.PostPedido(pedido);
                return CreatedAtAction(nameof(GetById), new { id = novoPedido.Id }, novoPedido);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Erro ao cadastrar o pedido " + e);
            }
        }
        [HttpPost("cliente")]
        public IActionResult PostComCliente(PedidoDtoCliente pedidoDto)
        {
            try
            {
                
                var pedido = _pedidoService.DtoToPedidoCliente(pedidoDto);
                var novoPedido = _pedidoService.PostPedido(pedido);
                return CreatedAtAction(nameof(GetById), new { id = novoPedido.Id }, novoPedido);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Erro ao cadastrar o pedido " + e);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, PedidoDto pedidoDto)
        {
            try
            {
                var pedido = _pedidoService.DtoToPedido(pedidoDto);
                _pedidoService.saveProdutoPedido(id, pedidoDto.Produtos);
                _pedidoService.UpdatePedido(id,pedido);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex);
                return NotFound(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Erro ao atualizar o pedido " + e);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            
            try
            {
                _pedidoService.DeletePedido(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Erro ao deletar o pedido " + e);
            }
        }
        

        [HttpPost("marcar/{id}")]
        public IActionResult Marcar(int id)
        {
            try
            {
                _pedidoService.MarcarPedido(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
