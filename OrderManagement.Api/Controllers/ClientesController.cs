using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.DTOs.Cliente;
using OrderManagement.Application.Interfaces;

namespace OrderManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        // O Controller apenas recebe a requisição e delega o trabalho para o Service.
        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        /// <summary>
        /// Retorna todos os clientes.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> ObterTodos()
        {
            var clientes = await _clienteService.ObterTodosAsync();

            return Ok(clientes);
        }

        /// <summary>
        /// Retorna um cliente pelo Id.
        /// </summary>
        [HttpGet("{id:guid}")]
        // ajuda o Swagger a documentar corretamente a API.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteDto>> ObterPorId(Guid id)
        {
            var cliente = await _clienteService.ObterPorIdAsync(id);

            return Ok(cliente);
        }

        /// <summary>
        /// Cadastra um novo cliente.
        /// </summary>
        [HttpPost]
        // ajuda o Swagger a documentar corretamente a API.
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Criar([FromBody] CreateClienteDto dto)
        {
            var id = await _clienteService.CriarAsync(dto);

            return CreatedAtAction(
                nameof(ObterPorId),
                new { id },
                id);
        }
    }
}
