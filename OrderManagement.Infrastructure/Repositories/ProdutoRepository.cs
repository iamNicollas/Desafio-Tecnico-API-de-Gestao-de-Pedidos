using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Persistence;

namespace OrderManagement.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Produto?> ObterPorIdAsync(Guid id)
        {
            return await _context.Produtos
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterTodosAsync()
        {
            return await _context.Produtos
                .AsNoTracking() // Como é uma consulta, não precisamos rastrear alterações.
                .ToListAsync();
        }

        public async Task AdicionarAsync(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);

            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Produto produto)
        {
            if (_context.Entry(produto).State == EntityState.Detached)
            {
                _context.Produtos.Attach(produto);
                _context.Entry(produto).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var produto = await ObterPorIdAsync(id);

            if (produto is null)
                return;

            _context.Produtos.Remove(produto);

            await _context.SaveChangesAsync();
        }
    }
}
