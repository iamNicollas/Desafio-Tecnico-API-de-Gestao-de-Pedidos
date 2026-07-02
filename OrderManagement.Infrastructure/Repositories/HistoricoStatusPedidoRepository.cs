using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Persistence;

namespace OrderManagement.Infrastructure.Repositories
{
    public class HistoricoStatusPedidoRepository : IHistoricoStatusPedidoRepository
    {
        private readonly ApplicationDbContext _context;

        public HistoricoStatusPedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(HistoricoStatusPedido historico)
        {
            await _context.HistoricoStatusPedido.AddAsync(historico);
            await _context.SaveChangesAsync();
        }
    }
}
