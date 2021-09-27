using ExemploApiItemCardapio.Entities;
using ExemploApiItemCardapio.Exceptions;
using ExemploApiItemCardapio.InputModel;
using ExemploApiItemCardapio.Repositories;
using ExemploApiItemCardapio.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemploApiItemCardapio.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<List<ItemViewModel>> Obter(int pagina, int quantidade)
        {
            var itens = await _itemRepository.Obter(pagina, quantidade);

            return itens.Select(item => new ItemViewModel
                                {
                                    Id = item.Id,
                                    Nome = item.Nome,
                                    Categoria = item.Categoria,
                                    Preco = item.Preco
                                })
                               .ToList();
        }

        public async Task<ItemViewModel> Obter(Guid id)
        {
            var item = await _itemRepository.Obter(id);

            if (item == null)
                return null;

            return new ItemViewModel
            {
                Id = item.Id,
                Nome = item.Nome,
                Categoria = item.Categoria,
                Preco = item.Preco
            };
        }

        public async Task<ItemViewModel> Inserir(ItemInputModel item)
        {
            var entidadeItem = await _itemRepository.Obter(item.Nome, item.Categoria);

            if (entidadeItem.Count > 0)
                throw new ItemJaCadastradoException();

            var itemInsert = new Item
            {
                Id = Guid.NewGuid(),
                Nome = item.Nome,
                Categoria = item.Categoria,
                Preco = item.Preco
            };

            await _itemRepository.Inserir(itemInsert);

            return new ItemViewModel
            {
                Id = itemInsert.Id,
                Nome = item.Nome,
                Categoria = item.Categoria,
                Preco = item.Preco
            };
        }

        public async Task Atualizar(Guid id, ItemInputModel item)
        {
            var entidadeItem = await _itemRepository.Obter(id);

            if (entidadeItem == null)
                throw new ItemNaoCadastradoException();

            entidadeItem.Nome = item.Nome;
            entidadeItem.Categoria = item.Categoria;
            entidadeItem.Preco = item.Preco;

            await _itemRepository.Atualizar(entidadeItem);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeItem = await _itemRepository.Obter(id);

            if (entidadeItem == null)
                throw new ItemNaoCadastradoException();

            entidadeItem.Preco = preco;

            await _itemRepository.Atualizar(entidadeItem);
        }

        public async Task Remover(Guid id)
        {
            var item = await _itemRepository.Obter(id);

            if (item == null)
                throw new ItemNaoCadastradoException();

            await _itemRepository.Remover(id);
        }

        public void Dispose()
        {
            _itemRepository?.Dispose();
        }
    }
}
