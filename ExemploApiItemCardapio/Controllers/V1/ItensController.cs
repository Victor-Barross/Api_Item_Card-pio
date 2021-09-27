using ExemploApiItemCardapio.Exceptions;
using ExemploApiItemCardapio.InputModel;
using ExemploApiItemCardapio.Services;
using ExemploApiItemCardapio.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExemploApiItemCardapio.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ItensController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItensController(IItemService itemService)
        {
            _itemService = itemService;
        }

        /// <summary>
        /// Buscar todos os itens no cardápio de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os itens no cardápio sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de item no cardápio</response>
        /// <response code="204">Caso não haja item no cardápio</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var itens = await _itemService.Obter(pagina, quantidade);

            if (itens.Count() == 0)
                return NoContent();

            return Ok(itens);
        }

        /// <summary>
        /// Buscar um item no cardápio pelo seu Id
        /// </summary>
        /// <param name="idItem">Id do item no cardápio buscado</param>
        /// <response code="200">Retorna o item no cardápio filtrado</response>
        /// <response code="204">Caso não haja item no cardápio com este id</response>   
        [HttpGet("{idItem:guid}")]
        public async Task<ActionResult<ItemViewModel>> Obter([FromRoute] Guid idItem)
        {
            var item = await _itemService.Obter(idItem);

            if (item == null)
                return NoContent();

            return Ok(item);
        }

        /// <summary>
        /// Inserir um item no cardápio
        /// </summary>
        /// <param name="itemInputModel">Dados do item no cardápio a ser inserido</param>
        /// <response code="200">Caso o item no cardápio seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um item no cardápio com mesmo nome para a mesma produtora</response>   
        [HttpPost]
        public async Task<ActionResult<ItemViewModel>> InserirItem([FromBody] ItemInputModel itemInputModel)
        {
            try
            {
                var item = await _itemService.Inserir(itemInputModel);

                return Ok(item);
            }
            catch (ItemJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um item no cardápio com este nome para esta categoria");
            }
        }

        /// <summary>
        /// Atualizar um item no cardápio
        /// </summary>
        /// /// <param name="idItem">Id do item no cardápio a ser atualizado</param>
        /// <param name="itemInputModel">Novos dados para atualizar o item no cardápio indicado</param>
        /// <response code="200">Caso o item no cardápio seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um item no cardápio com este Id</response>   
        [HttpPut("{idItem:guid}")]
        public async Task<ActionResult> AtualizarItem([FromRoute] Guid idItem, [FromBody] ItemInputModel itemInputModel)
        {
            try
            {
                await _itemService.Atualizar(idItem, itemInputModel);

                return Ok();
            }
            catch (ItemNaoCadastradoException ex)
            {
                return NotFound("Não existe este item no cardápio");
            }
        }

        /// <summary>
        /// Atualizar o preço de um item no cardápio
        /// </summary>
        /// /// <param name="idItem">Id do item no cardápio a ser atualizado</param>
        /// <param name="preco">Novo preço do item no cardápio</param>
        /// <response code="200">Caso o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um item no cardápio com este Id</response>   
        [HttpPatch("{idItem:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarItem([FromRoute] Guid idItem, [FromRoute] double preco)
        {
            try
            {
                await _itemService.Atualizar(idItem, preco);

                return Ok();
            }
            catch (ItemNaoCadastradoException ex)
            {
                return NotFound("Não existe este item no cardápio");
            }
        }

        /// <summary>
        /// Excluir um item no cardápio
        /// </summary>
        /// /// <param name="idItem">Id do item no cardápio a ser excluído</param>
        /// <response code="200">Caso o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um item no cardápio com este Id</response>   
        [HttpDelete("{idItem:guid}")]
        public async Task<ActionResult> ApagarItem([FromRoute] Guid idItem)
        {
            try
            {
                await _itemService.Remover(idItem);

                return Ok();
            }
            catch (ItemNaoCadastradoException ex)
            {
                return NotFound("Não existe este item no cardápio");
            }
        }

    }
}
