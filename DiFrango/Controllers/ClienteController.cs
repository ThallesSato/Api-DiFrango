using DiFrango.Models;
using DiFrango.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DiFrango.Controllers
{
    [Route("api/Cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var clientes = _clienteService.GetAllClientes();
            return Ok(clientes);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var cliente = _clienteService.GetByIdCliente(id);
                return Ok(cliente);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        } 
        [HttpGet("telefone/{telefone}")]
        public IActionResult GetByTelefone(string telefone)
        {
            try
            {
                var clientes = _clienteService.GetByTelefoneCliente(telefone);
                return Ok(clientes);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        } 
        [HttpGet("find/{str}")]
        public IActionResult GetAllByTelefoneOrNomeCliente(string str)
        {
            try
            {
                var clientes = _clienteService.GetAllByTelefoneOrNomeCliente(str);
                return Ok(clientes);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        } 
        [HttpPost]
        public IActionResult Post(Cliente cliente)
        {
            try
            {
                var novoCliente = _clienteService.PostCliente(cliente);
                return CreatedAtAction(nameof(GetById), new { id = novoCliente.Id }, novoCliente);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Erro ao cadastrar cliente");
            }
        } 
        [HttpPut("{id}")]
        public IActionResult Update(int id, Cliente cliente)
        {
            try
            {
                _clienteService.UpdateCliente(id,cliente);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        } 
    }
}