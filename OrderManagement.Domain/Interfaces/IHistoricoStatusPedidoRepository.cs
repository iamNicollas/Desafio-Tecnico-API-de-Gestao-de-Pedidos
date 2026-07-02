using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Interfaces
{
    public interface IHistoricoStatusPedidoRepository
    {
        Task AdicionarAsync(HistoricoStatusPedido historico);
    }
}
