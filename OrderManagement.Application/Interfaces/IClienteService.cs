using OrderManagement.Application.DTOs.Cliente;

namespace OrderManagement.Application.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteDto> ObterPorIdAsync(Guid id);

        Task<IEnumerable<ClienteDto>> ObterTodosAsync();

        Task<Guid> CriarAsync(CreateClienteDto dto);

        Task AtualizarAsync(Guid id, UpdateClienteDto dto);

        Task RemoverAsync(Guid id);
    }
}
