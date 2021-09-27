using ExemploApiItemCardapio.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExemploApiItemCardapio.Repositories
{
    public interface IItemRepository : IDisposable
    {
        Task<List<Item>> Obter(int pagina, int quantidade);
        Task<Item> Obter(Guid id);
        Task<List<Item>> Obter(string nome, string produtora);
        Task Inserir(Item item);
        Task Atualizar(Item item);
        Task Remover(Guid id);
    }
}
