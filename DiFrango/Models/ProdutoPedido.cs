using System.Text.Json.Serialization;

namespace DiFrango.Models;
public class ProdutoPedido
{
    [JsonIgnore]
    public int PedidoId { get; set; }
    [JsonIgnore]
    public int ProdutoId { get; set; }
    [JsonIgnore]
    public Pedido Pedido { get; set; }
    public Produto Produto { get; set; }
    public float Quantidade{ get; set; }
}