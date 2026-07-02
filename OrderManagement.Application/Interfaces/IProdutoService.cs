using OrderManagement.Application.DTOs.Produto;

namespace OrderManagement.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<ProdutoDto> ObterPorIdAsync(Guid id);

        Task<IEnumerable<ProdutoDto>> ObterTodosAsync();

        Task<Guid> CriarAsync(CreateProdutoDto dto);

        Task AtualizarAsync(Guid id, UpdateProdutoDto dto);

        Task RemoverAsync(Guid id);
    }
}
