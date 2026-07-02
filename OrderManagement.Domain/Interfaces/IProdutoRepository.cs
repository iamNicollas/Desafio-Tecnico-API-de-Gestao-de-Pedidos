using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produto?> ObterPorIdAsync(Guid id);

        Task<IEnumerable<Produto>> ObterTodosAsync();

        Task AdicionarAsync(Produto produto);

        Task AtualizarAsync(Produto produto);

        Task RemoverAsync(Guid id);
    }
}
