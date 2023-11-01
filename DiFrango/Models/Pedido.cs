using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DiFrango.Models;

public class Pedido
{
    [Key]
    public int Id { get; set; }
    public Cliente Cliente { get; set; }
    public DateTime DataHoraPedido { get; set; }
    public bool Marcado { get; set; }
    public DateTime DataHoraCriado{ get; set; }
    public List<ProdutoPedido> Produtos { get; set; }
    [JsonIgnore]
    public bool Deletado { get; set; }

    public Pedido()
    {
        Produtos = new List<ProdutoPedido>();
        Deletado = false;
        DataHoraCriado = DateTime.Now;
    }
    public void Update(Pedido pedido)
    {
        if (pedido.Cliente != null)
        {
            Cliente = pedido.Cliente;
        }
        DataHoraPedido = pedido.DataHoraPedido;
    }

    public void Marcar()
    {
        Marcado = true;
    }

    public void Deletar()
    {
        Deletado = true;
    }
}