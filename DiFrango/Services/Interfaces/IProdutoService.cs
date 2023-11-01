using DiFrango.Models;

namespace DiFrango.Services.Interfaces
{
    public interface IProdutoService
    {
        Produto GetByIdProduto(int id);
    }
}