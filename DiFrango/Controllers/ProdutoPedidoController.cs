using DiFrango.Dtos;
using DiFrango.Services;
using DiFrango.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DiFrango.Controllers
{
    [Route("api/PedidosPRODUTOS")]
    [ApiController]
    public class ProdutoPedidoController : ControllerBase
    {
        private readonly IProdutoPedidoService _produtoPedidoService;

        public ProdutoPedidoController(IProdutoPedidoService produtoPedidoService)
        {
            _produtoPedidoService = produtoPedidoService;
        }

        [HttpPost("id")]
        public IActionResult Post(int pedidoId, List<ProdutoPedidoDto> produtoPedidoDtos)
        {
            try
            {
                var pedido = _produtoPedidoService.AddProdutos(pedidoId, produtoPedidoDtos);
                return CreatedAtAction(nameof(PedidoController.GetById), "Pedido",new { id = pedido.Id }, pedido);
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
        
        [HttpPut("{pedidoId}")]
        public IActionResult Put(int pedidoId, List<ProdutoPedidoDto> produtoPedidoDtos)
        {
            try
            {
                _produtoPedidoService.AddProdutos(pedidoId, produtoPedidoDtos);
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
    }
}