using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        Task<Pedido?> ObterPorIdAsync(Guid id);

        Task<IEnumerable<Pedido>> ObterTodosAsync();

        Task AdicionarAsync(Pedido pedido);

        Task AtualizarAsync(Pedido pedido);
    }
}
