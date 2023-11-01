using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace DiFrango.Models;

public class Cliente
{
    [Key]
    public int Id{ get; set; }
    public string? Nome{ get; set;}
    public string? Telefone{ get; set;}
    public string? Endereco{ get; set;}

    public void Update(Cliente cliente)
    {
        Nome = cliente.Nome;
        Telefone = cliente.Telefone;
        Endereco = cliente.Endereco;
    }
}