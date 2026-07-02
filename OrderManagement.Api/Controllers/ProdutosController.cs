using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.DTOs.Produto;
using OrderManagement.Application.Interfaces;

namespace OrderManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        // O Controller apenas recebe a requisição e delega para o Service.
        public ProdutosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        /// <summary>
        /// Retorna todos os produtos.
        /// </summary>
        [HttpGet]
        // ajuda o Swagger a documentar corretamente a API.
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> ObterTodos()
        {
            var produtos = await _produtoService.ObterTodosAsync();

            return Ok(produtos);
        }

        /// <summary>
        /// Retorna um produto pelo Id.
        /// </summary>
        [HttpGet("{id:guid}")]
        // ajuda o Swagger a documentar corretamente a API.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProdutoDto>> ObterPorId(Guid id)
        {
            var produto = await _produtoService.ObterPorIdAsync(id);

            return Ok(produto);
        }

        /// <summary>
        /// Cadastra um novo produto.
        /// </summary>
        [HttpPost]
        // ajuda o Swagger a documentar corretamente a API
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Criar([FromBody] CreateProdutoDto dto)
        {
            var id = await _produtoService.CriarAsync(dto);

            return CreatedAtAction(nameof(ObterPorId), new { id }, id);
        }
    }
}
