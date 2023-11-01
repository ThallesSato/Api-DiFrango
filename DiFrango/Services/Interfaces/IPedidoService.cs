using DiFrango.Dtos;
using DiFrango.Models;

namespace DiFrango.Services.Interfaces
{
    public interface IPedidoService
    {
        List<Pedido> GetAllPedidos();
        List<Pedido> GetAllByDatePedidos(DateTime date);
        List<Pedido> GetAllByDateAndCategoriaPedidos(DateTime date, int categoriaId);
        List<Pedido> GetAllByDateAndCategoriaPedidos(DateTime date, int categoriaId, int categoriaId2);
        Pedido GetByIdPedido(int id);
        Pedido PostPedido(Pedido pedido);
        void UpdatePedido(int id, Pedido pedido);
        void DeletePedido(int id);
        void MarcarPedido(int id);
        Pedido DtoToPedido(PedidoDto pedidoDto);
        Pedido DtoToPedidoCliente(PedidoDtoCliente pedidoDto);
        void saveProdutoPedido(int id, List<ProdutoPedidoDto> produtoPedidos);
        List<PedidoResponse> GetByClientePedido(int id);
    }
}