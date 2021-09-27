using ExemploApiItemCardapio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemploApiItemCardapio.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private static Dictionary<Guid, Item> itens = new Dictionary<Guid, Item>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Item{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Nome = "Tokyo", Categoria = "Hambúrguer", Preco = 29} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Item{ Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Nome = "Berlin", Categoria = "Hambúrguer", Preco = 19} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Item{ Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Nome = "New York", Categoria = "Hambúrguer", Preco = 18} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Item{ Id = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Nome = "Água Mineral - 500ml", Categoria = "Bebidas", Preco = 4} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Item{ Id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Nome = "Fanta - 350ml", Categoria = "Bebidas", Preco = 8} },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Item{ Id = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Nome = "Coca cola - 350ml", Categoria = "Bebidas", Preco = 8} }
        };

        public Task<List<Item>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(itens.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Item> Obter(Guid id)
        {
            if (!itens.ContainsKey(id))
                return Task.FromResult<Item>(null);

            return Task.FromResult(itens[id]);
        }

        public Task<List<Item>> Obter(string nome, string categoria)
        {
            return Task.FromResult(itens.Values.Where(item => item.Nome.Equals(nome) && item.Categoria.Equals(categoria)).ToList());
        }

        public Task<List<Item>> ObterSemLambda(string nome, string categoria)
        {
            var retorno = new List<Item>();

            foreach(var item in itens.Values)
            {
                if (item.Nome.Equals(nome) && item.Categoria.Equals(categoria))
                    retorno.Add(item);
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(Item item)
        {
            itens.Add(item.Id, item);
            return Task.CompletedTask;
        }

        public Task Atualizar(Item item)
        {
            itens[item.Id] = item;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            itens.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Fechar conexão com o banco
        }
    }
}
