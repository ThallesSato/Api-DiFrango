using DiFrango.Dtos;
using DiFrango.Models;

namespace DiFrango.Services.Interfaces
{
    public interface IProdutoPedidoService
    {
        List<ProdutoPedido> ListDtoToListProdutoPedido(List<ProdutoPedidoDto> produtoPedidoDtos);
        Pedido AddProdutos(int pedidoId, List<ProdutoPedidoDto> produtoPedidoDtos);
        List<ProdutoPedido> AddProdutoPedidosExistente(int pedidoId, List<ProdutoPedido> produtoPedidos);
    }
}