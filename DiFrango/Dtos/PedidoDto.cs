using DiFrango.Models;

namespace DiFrango.Dtos;

public class PedidoDto
{
    public int Cliente { get; set; }
    public DateTime DataHoraPedido { get; set; }
    public List<ProdutoPedidoDto>? Produtos { get; set; }
}