using DiFrango.Database;
using DiFrango.Dtos;
using DiFrango.Models;
using DiFrango.Services.Interfaces;

namespace DiFrango.Services;

public class ClienteService : IClienteService
{
    private readonly AppDbContext _context;
    public ClienteService(AppDbContext context)
    {
        _context = context;
    }
    
    public List<Cliente> GetAllClientes()
    {
        var clientes = _context.Clientes.ToList();
        return clientes;    
    }
    public Cliente GetByIdCliente(int id)
    {
        var cliente = _context.Clientes.SingleOrDefault(d => d.Id==id);
        return cliente ?? throw new KeyNotFoundException("Cliente não encontrado");
    }
    public Cliente? GetByTelefoneCliente(string telefone)
    {
        var cliente = _context.Clientes.SingleOrDefault(d => d.Telefone==telefone);
        return cliente;
    }
    public List<Cliente> GetAllByTelefoneOrNomeCliente(string str)
    {
        return _context.Clientes.Where(c => c.Telefone.Contains(str) || c.Nome.Contains(str)).ToList();
    }
    public Cliente PostCliente(Cliente cliente)
    {
        if (IsTelefoneInBd(cliente.Telefone))
        {
            throw new InvalidOperationException("Telefone já cadastrado");
        }
        _context.Clientes.Add(cliente);
        _context.SaveChanges();

        return cliente;
    } 
    public Cliente PostClienteDto(ClienteDto clienteDto)
    {
        if (IsTelefoneInBd(clienteDto.Telefone))
        {
            throw new InvalidOperationException("Telefone já cadastrado");
        }
        Cliente cliente = new Cliente();
        cliente.Telefone = clienteDto.Telefone;
        cliente.Nome = clienteDto.Nome;
        cliente.Endereco = clienteDto.Endereco;
        _context.Clientes.Add(cliente);
        _context.SaveChanges();

        return cliente;
    } 
    public void UpdateCliente(int id, Cliente cliente)
    {
        var oldCliente = GetByIdCliente(id);
        oldCliente.Update(cliente);
        _context.Clientes.Update(oldCliente);
        _context.SaveChanges();
    } 
    
    
    public bool IsTelefoneInBd(string telefone)
    {
        return _context.Clientes.Any(c => c.Telefone == telefone);
    }
}