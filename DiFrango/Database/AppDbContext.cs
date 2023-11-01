using System.Collections.Immutable;
using DiFrango.Models;
using Microsoft.EntityFrameworkCore;

namespace DiFrango.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<ProdutoPedido> ProdutosPedidos { get; set; }
    

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Cliente>(e =>
        {
            e.HasIndex(e => e.Telefone).IsUnique();
        });
        
        builder.Entity<ProdutoPedido>(e =>
        {
            e.HasKey(e => new { e.ProdutoId, e.PedidoId });
        });
    }
}