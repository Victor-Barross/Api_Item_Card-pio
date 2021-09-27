using ExemploApiItemCardapio.InputModel;
using ExemploApiItemCardapio.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExemploApiItemCardapio.Services
{
    public interface IItemService : IDisposable
    {
        Task<List<ItemViewModel>> Obter(int pagina, int quantidade);
        Task<ItemViewModel> Obter(Guid id);
        Task<ItemViewModel> Inserir(ItemInputModel item);
        Task Atualizar(Guid id, ItemInputModel item);
        Task Atualizar(Guid id, double preco);
        Task Remover(Guid id);
    }
}
