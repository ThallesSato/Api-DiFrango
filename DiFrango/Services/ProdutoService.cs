using DiFrango.Database;
using DiFrango.Dtos;
using DiFrango.Models;
using DiFrango.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiFrango.Services;

public class ProdutoService : IProdutoService
{
    private readonly AppDbContext _context;

    public ProdutoService(AppDbContext context)
    {
        _context = context;
    }

    public Produto GetByIdProduto(int id)
    {
        return _context.Produtos.SingleOrDefault(d => d.Id==id) ?? throw new InvalidOperationException("Produto não encontrado");
    }

}