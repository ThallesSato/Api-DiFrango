using DiFrango.Database;
using DiFrango.Dtos;
using DiFrango.Models;
using DiFrango.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiFrango.Services;

public class PedidoService : IPedidoService
{
    private readonly AppDbContext _context;
    private readonly IClienteService _clienteService;
    private readonly IServiceProvider _services;
    public PedidoService(AppDbContext context, IClienteService clienteService, IServiceProvider services)
    {
        _context = context;
        _clienteService = clienteService;
        _services = services;
    }

    
    public List<Pedido> GetAllPedidos()
    {
        return _context.Pedidos
            .Include(p => p.Cliente) // Carrega a propriedade de navegação Cliente
            .Include(p => p.Produtos) // Carrega a propriedade de navegação Produtos
            .ThenInclude(pp => pp.Produto.Categoria) // Carrega a propriedade de navegação Produto dentro de Produtos
            .ToList();
    }
    public List<Pedido> GetAllByDatePedidos(DateTime date)
    {
        return _context.Pedidos.Where(d => d.DataHoraPedido.Date == date.Date && d.Deletado == false)
            .Include(p => p.Cliente) 
            .Include(p => p.Produtos) 
            .ThenInclude(pp => pp.Produto.Categoria)
            .ToList();
    }
    public List<Pedido> GetAllByDateAndCategoriaPedidos(DateTime date, int categoriaId)
    {
        return _context.Pedidos.Where(d => d.DataHoraPedido.Date == date.Date && d.Deletado == false)
            .Include(p => p.Cliente) 
            .Include(p => p.Produtos) 
            .ThenInclude(pp => pp.Produto.Categoria)
            .Where(p => p.Produtos.Any(pp => pp.Produto.Categoria.Id == categoriaId))
            .ToList();
    }
    public List<Pedido> GetAllByDateAndCategoriaPedidos(DateTime date, int categoriaId,int categoriaId2)
    {
        return _context.Pedidos.Where(d => d.DataHoraPedido.Date == date.Date && d.Deletado == false)
            .Include(p => p.Cliente) 
            .Include(p => p.Produtos) 
            .ThenInclude(pp => pp.Produto.Categoria)
            .Where(p => p.Produtos.Any(pp => pp.Produto.Categoria.Id == categoriaId || pp.Produto.Categoria.Id == categoriaId2))
            .ToList();
    }
    public Pedido GetByIdPedido(int id)
    {
        return _context.Pedidos
            .Include(p => p.Cliente) 
            .Include(p => p.Produtos) 
            .ThenInclude(pp => pp.Produto.Categoria)
            .SingleOrDefault(d => d.Id==id) ?? throw new InvalidOperationException("Pedido não encontrado");
    }

    
    
    public Pedido PostPedido(Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
        _context.SaveChanges();
        return pedido;
    }
    public void UpdatePedido(int id, Pedido pedido)
    {
        var oldPedido = GetByIdPedido(id);
        oldPedido.Update(pedido);
        _context.Pedidos.Update(oldPedido);
        _context.SaveChanges();
    }
    public void DeletePedido(int id)
    {
        var pedido = GetByIdPedido(id);
        pedido.Deletar();
        _context.Pedidos.Update(pedido);
        _context.SaveChanges();
    }
    
    

    public void MarcarPedido(int id)
    {
        var pedido = GetByIdPedido(id);
        pedido.Marcar();
        _context.Pedidos.Update(pedido);
        _context.SaveChanges();
    }

    public Pedido DtoToPedido(PedidoDto pedidoDto)
    {
        var pedido = new Pedido();
        if (pedidoDto.Cliente == null || pedidoDto.Cliente == 0 )
        {
        }else
        {
            pedido.Cliente = _clienteService.GetByIdCliente(pedidoDto.Cliente);
        }
        
        pedido.DataHoraPedido = pedidoDto.DataHoraPedido;
        if (pedidoDto.Produtos != null)
        {
            var produtoPedidoService = _services.GetRequiredService<IProdutoPedidoService>();
            pedido.Produtos.AddRange(produtoPedidoService.ListDtoToListProdutoPedido(pedidoDto.Produtos));
        }
        return pedido;
    }

    public Pedido DtoToPedidoCliente(PedidoDtoCliente pedidoDto)
    {
        var pedido = new Pedido();
        if (pedidoDto.Cliente.Telefone != null){
            if (_clienteService.IsTelefoneInBd(pedidoDto.Cliente.Telefone))
            {
                pedido.Cliente = _clienteService.GetByTelefoneCliente(pedidoDto.Cliente.Telefone);
            }
            else
            {
                pedido.Cliente = _clienteService.PostClienteDto(pedidoDto.Cliente);
            }
            pedido.DataHoraPedido = pedidoDto.DataHoraPedido;
            if (pedidoDto.Produtos != null)
            {
                var produtoPedidoService = _services.GetRequiredService<IProdutoPedidoService>();
                pedido.Produtos.AddRange(produtoPedidoService.ListDtoToListProdutoPedido(pedidoDto.Produtos));
            }
        }
        return pedido;
    }

    public void saveProdutoPedido(int pedidoId, List<ProdutoPedidoDto> produtoPedidoDtos)
    {
        var produtoPedidoService = _services.GetRequiredService<IProdutoPedidoService>();
        produtoPedidoService.AddProdutos(pedidoId, produtoPedidoDtos);
    }

    public List<PedidoResponse> GetByClientePedido(int id)
    {
        var pedidos = _context.Pedidos.Where(d => d.Cliente.Id == id)
            .Include(p => p.Cliente) 
            .Include(p => p.Produtos) 
            .ThenInclude(pp => pp.Produto.Categoria).ToList();
        
        var pedidosDto = pedidos.Select(pedido => new PedidoResponse
        {
            Id = pedido.Id,
            Cliente = pedido.Cliente,
            DataHoraPedido = pedido.DataHoraPedido,
            Marcado = pedido.Marcado,
            DataHoraCriado = pedido.DataHoraCriado,
            Produtos = pedido.Produtos,
            Deletado = pedido.Deletado 
        }).ToList();

        return pedidosDto;
    }
}