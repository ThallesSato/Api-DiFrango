using System.ComponentModel.DataAnnotations;

namespace DiFrango.Models;

public class Produto
{
    [Key]
    public int Id { get; set; }
    public string Nome  { get; set; }
    public Categoria Categoria { get; set; }
    
}