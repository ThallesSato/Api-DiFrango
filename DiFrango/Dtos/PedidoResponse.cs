using DiFrango.Models;

namespace DiFrango.Dtos;

public class PedidoResponse
{
    
    public int Id { get; set; }
    public Cliente Cliente { get; set; }
    public DateTime DataHoraPedido { get; set; }
    public bool Marcado { get; set; }
    public DateTime DataHoraCriado{ get; set; }
    public List<ProdutoPedido> Produtos { get; set; }
    public bool Deletado { get; set; }
}