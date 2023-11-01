using DiFrango.Database;
using DiFrango.Dtos;
using DiFrango.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiFrango.Controllers
{
    [Route("api/Produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var produtos = _context.Produtos.Include(p => p.Categoria);
            return Ok(produtos);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var produto = _context.Produtos.Include(p => p.Categoria).SingleOrDefault(d => d.Id==id);
            return Ok(produto);
        }
        [HttpPost]
        public IActionResult Post(ProdutoDto produtoDto)
        {
            var novoProduto = new Produto();
            novoProduto.Nome = produtoDto.Nome;
            var cat = _context.Categorias.SingleOrDefault(c => c.Nome == produtoDto.Categoria);
            novoProduto.Categoria = cat; 
            if(cat == null){
                var cate = new Categoria();
                cate.Nome = produtoDto.Categoria;
                novoProduto.Categoria = cate;
            }
            Console.WriteLine(novoProduto.ToString());
            _context.Produtos.Add(novoProduto);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = novoProduto.Id }, novoProduto);
        } 
    }
}
