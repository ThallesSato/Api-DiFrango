using DiFrango.Database;
using DiFrango.Dtos;
using DiFrango.Models;
using DiFrango.Services.Interfaces;

namespace DiFrango.Services;

public class ProdutoPedidoService : IProdutoPedidoService
{
    private readonly AppDbContext _context;
    private readonly IProdutoService _produtoService;
    private readonly IPedidoService _pedidoService;
    public ProdutoPedidoService(AppDbContext context, IProdutoService produtoService, IPedidoService pedidoService)
    {
        _context = context;
        _produtoService = produtoService;
        _pedidoService = pedidoService;
    }
    
    public List<ProdutoPedido> ListDtoToListProdutoPedido(List<ProdutoPedidoDto> produtoPedidoDtos)
    {
        List<ProdutoPedido>? produtoPedidos = new();
        foreach (var produtoPedidoDto in produtoPedidoDtos)
        {
            var produtoPedido = new ProdutoPedido();
            produtoPedido.Produto = _produtoService.GetByIdProduto(produtoPedidoDto.ProdutoId);
            produtoPedido.Quantidade = produtoPedidoDto.Quantidade;
            produtoPedidos.Add(produtoPedido);
        }
        return produtoPedidos;
    }

    public Pedido AddProdutos(int pedidoId, List<ProdutoPedidoDto> produtoPedidoDtos)
    {
        var pedido = _pedidoService.GetByIdPedido(pedidoId);
        var produtoPedidos = AddProdutoPedidosExistente(pedidoId, ListDtoToListProdutoPedido(produtoPedidoDtos));
        if (produtoPedidos.Any())
        {
            pedido.Produtos.AddRange(produtoPedidos);
            _pedidoService.UpdatePedido(pedidoId, pedido);
            _context.SaveChanges();
        }
        return pedido;
    }

    public List<ProdutoPedido> AddProdutoPedidosExistente(int pedidoId, List<ProdutoPedido> produtoPedidos)
    {
        var pedido = _pedidoService.GetByIdPedido(pedidoId);
        var resultProdutosPedidos = new List<ProdutoPedido>();
        foreach (var produtoPedido in produtoPedidos)
        {
            if (_context.ProdutosPedidos.Any(pp => pp.Produto.Id == produtoPedido.Produto.Id && pp.Pedido.Id == pedido.Id))
            {
                var produtoPedidoExistente = _context.ProdutosPedidos.SingleOrDefault(p =>
                    p.Produto.Id == produtoPedido.Produto.Id && 
                    p.Pedido.Id == pedido.Id);

                if (produtoPedido.Quantidade == 0)
                {
                    _context.ProdutosPedidos.Remove(produtoPedidoExistente);
                }
                else
                {
                    produtoPedidoExistente.Quantidade = produtoPedido.Quantidade;
                    _context.ProdutosPedidos.Update(produtoPedidoExistente);
                }

            }
            else
            {
                resultProdutosPedidos.Add(produtoPedido);
            }
        }
        _context.SaveChanges();
        return resultProdutosPedidos;
    }
}