using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Persistence;


namespace OrderManagement.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly ApplicationDbContext _context;

        public PedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido?> ObterPorIdAsync(Guid id)
        {
            return await _context.Pedidos
                // Carrega o Cliente do pedido.
                .Include(p => p.Cliente) // busca o cliente relacionado

                // Carrega os Itens do pedido.
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto) //busca itens do pedido e, para cada item, busca também o produto.

                // Carrega o histórico de alterações de status.
                .Include(p => p.Historico)
                .AsSplitQuery()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Pedido>> ObterTodosAsync()
        {
            return await _context.Pedidos
                .AsNoTracking()

                .Include(p => p.Cliente)

                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)

                .Include(p => p.Historico)

                .ToListAsync();
        }

        public async Task AdicionarAsync(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);

            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Pedido pedido)
        {
            await _context.SaveChangesAsync();
        }
    }
}
