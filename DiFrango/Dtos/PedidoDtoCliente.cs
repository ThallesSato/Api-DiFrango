using DiFrango.Models;

namespace DiFrango.Dtos;

public class PedidoDtoCliente
{
    public ClienteDto Cliente { get; set; }
    public DateTime DataHoraPedido { get; set; }
    public List<ProdutoPedidoDto>? Produtos { get; set; }
}