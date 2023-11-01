using DiFrango.Dtos;
using DiFrango.Models;

namespace DiFrango.Services.Interfaces
{
    public interface IClienteService
    {
        List<Cliente> GetAllClientes();
        Cliente GetByIdCliente(int id);
        Cliente? GetByTelefoneCliente(string telefone);
        List<Cliente> GetAllByTelefoneOrNomeCliente(string str);
        Cliente PostCliente(Cliente cliente);
        Cliente PostClienteDto(ClienteDto clienteDto);
        void UpdateCliente(int id, Cliente cliente);
        bool IsTelefoneInBd(string telefone);
    }
}