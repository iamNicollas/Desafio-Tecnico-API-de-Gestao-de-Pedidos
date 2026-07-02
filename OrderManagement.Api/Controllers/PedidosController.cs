using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.DTOs.Pedido;
using OrderManagement.Application.Interfaces;

namespace OrderManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        // O Controller apenas recebe a requisição e delega a regra de negócio para o Service.
        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        /// <summary>
        /// Retorna todos os pedidos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> ObterTodos()
        {
            var pedidos = await _pedidoService.ObterTodosAsync();

            return Ok(pedidos);
        }

        /// <summary>
        /// Retorna um pedido pelo Id.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PedidoDto>> ObterPorId(Guid id)
        {
            var pedido = await _pedidoService.ObterPorIdAsync(id);

            return Ok(pedido);
        }

        /// <summary>
        /// Cria um novo pedido.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Criar([FromBody] CreatePedidoDto dto)
        {
            var id = await _pedidoService.CriarAsync(dto);

            return CreatedAtAction(nameof(ObterPorId), new { id }, id);
        }

        /// <summary>
        /// Marca um pedido como pago.
        /// </summary>
        [HttpPatch("{id:guid}/pagar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MarcarComoPago(Guid id)
        {
            await _pedidoService.MarcarComoPagoAsync(id);

            return NoContent();
        }

        /// <summary>
        /// Marca um pedido como enviado.
        /// </summary>
        [HttpPatch("{id:guid}/enviar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MarcarComoEnviado(Guid id)
        {
            await _pedidoService.MarcarComoEnviadoAsync(id);

            return NoContent();
        }

        /// <summary>
        /// Cancela um pedido.
        /// </summary>
        [HttpPatch("{id:guid}/cancelar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Cancelar( Guid id, [FromBody] CancelarPedidoDto dto)
        {
            await _pedidoService.CancelarAsync(id, dto.Motivo);

            return NoContent();
        }
    }
}
