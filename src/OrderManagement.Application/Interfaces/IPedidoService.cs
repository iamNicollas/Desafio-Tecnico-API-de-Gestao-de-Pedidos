using OrderManagement.Application.DTOs.Pedido;

namespace OrderManagement.Application.Interfaces
{
    public interface IPedidoService
    {
        Task<PedidoDto> ObterPorIdAsync(Guid id);

        Task<IEnumerable<PedidoDto>> ObterTodosAsync();

        Task<Guid> CriarAsync(CreatePedidoDto dto);

        Task MarcarComoPagoAsync(Guid pedidoId);

        Task MarcarComoEnviadoAsync(Guid pedidoId);

        Task CancelarAsync(Guid pedidoId, string? motivo);
    }
}
