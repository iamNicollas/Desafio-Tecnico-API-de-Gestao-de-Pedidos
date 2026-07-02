using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Persistence;
using System.Text.RegularExpressions;

namespace OrderManagement.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente?> ObterPorIdAsync(Guid id)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> ExisteDocumentoAsync(string documento)
        {
            documento = Regex.Replace(documento, @"\D", "");

            var documentos = await _context.Clientes
                .AsNoTracking()
                .Select(c => c.Documento)
                .ToListAsync();

            return documentos.Any(d => d.Numero == documento);
        }

        public async Task<IEnumerable<Cliente>> ObterTodosAsync()
        {
            return await _context.Clientes
                .AsNoTracking() // Consulta somente leitura, melhor para performance.
                .ToListAsync();
        }

        public async Task AdicionarAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);

            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Cliente cliente)
        {
            if (_context.Entry(cliente).State == EntityState.Detached)
            {
                _context.Clientes.Attach(cliente);
                _context.Entry(cliente).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var cliente = await ObterPorIdAsync(id);

            if (cliente is null)
                return;

            _context.Clientes.Remove(cliente);

            await _context.SaveChangesAsync();
        }
    }
}
