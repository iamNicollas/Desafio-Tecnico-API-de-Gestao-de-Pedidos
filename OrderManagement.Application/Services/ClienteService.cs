using OrderManagement.Application.DTOs.Cliente;
using OrderManagement.Application.Interfaces;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Exceptions;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Application.Services
{
    // Principio SOLID - DIP de depender da abstração e não da implementação
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<ClienteDto>> ObterTodosAsync()
        {
            var clientes = await _clienteRepository.ObterTodosAsync();

            return clientes.Select(cliente => new ClienteDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email.ToString(),
                Documento = cliente.Documento.ToString(),
                Ativo = cliente.Ativo
            });
        }

        public async Task<ClienteDto> ObterPorIdAsync(Guid id)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(id);

            if (cliente is null)
                throw new DomainException("Cliente não encontrado.");

            return new ClienteDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email.ToString(),
                Documento = cliente.Documento.ToString(),
                Ativo = cliente.Ativo
            };
        }

        public async Task<Guid> CriarAsync(CreateClienteDto dto)
        {
            if (await _clienteRepository.ExisteDocumentoAsync(dto.Documento))
                throw new DomainException("Já existe um cliente cadastrado com este documento.");

            var cliente = new Cliente(
                dto.Nome,
                new Email(dto.Email),
                new Documento(dto.Documento));

            await _clienteRepository.AdicionarAsync(cliente);

            return cliente.Id;
        }

        public async Task AtualizarAsync(Guid id, UpdateClienteDto dto)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(id);

            if (cliente is null)
                throw new DomainException("Cliente não encontrado.");

            // Domain - Quem modifica o estado da entidade é a própria entidade.
            cliente.AlterarNome(dto.Nome); 
            cliente.AlterarEmail(new Email(dto.Email));

            await _clienteRepository.AtualizarAsync(cliente);
        }

        public async Task RemoverAsync(Guid id)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(id);

            if (cliente is null)
                throw new DomainException("Cliente não encontrado.");

            await _clienteRepository.RemoverAsync(id);
        }
    }
}
