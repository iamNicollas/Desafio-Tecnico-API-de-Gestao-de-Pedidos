using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente?> ObterPorIdAsync(Guid id);

        Task<bool> ExisteDocumentoAsync(string documento);

        Task<IEnumerable<Cliente>> ObterTodosAsync();

        Task AdicionarAsync(Cliente cliente);

        Task AtualizarAsync(Cliente cliente);

        Task RemoverAsync(Guid id);
    }
}
