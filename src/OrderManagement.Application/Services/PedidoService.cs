using OrderManagement.Application.DTOs.Pedido;
using OrderManagement.Application.Interfaces;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Exceptions;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Services
{
    // Principio SOLID - DIP de depender da abstração e não da implementação
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IHistoricoStatusPedidoRepository _historicoRepository;

        public PedidoService(
            IPedidoRepository pedidoRepository,
            IClienteRepository clienteRepository,
            IProdutoRepository produtoRepository,
            IHistoricoStatusPedidoRepository historicoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
            _historicoRepository = historicoRepository;
        }

        public async Task<Guid> CriarAsync(CreatePedidoDto dto)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(dto.ClienteId);

            if (cliente is null)
                throw new DomainException("Cliente não encontrado.");

            if (!cliente.PodeCriarPedido())
                throw new DomainException("Cliente inativo.");

            var pedido = new Pedido(cliente.Id);

            foreach (var itemDto in dto.Itens)
            {
                var produto = await _produtoRepository.ObterPorIdAsync(itemDto.ProdutoId);

                if (produto is null)
                    throw new DomainException($"Produto {itemDto.ProdutoId} não encontrado.");

                if (!produto.PossuiEstoque(itemDto.Quantidade))
                    throw new DomainException($"Estoque insuficiente para o produto {produto.Nome}.");

                produto.DebitarEstoque(itemDto.Quantidade);

                var item = new PedidoItem(
                    produto.Id,
                    itemDto.Quantidade,
                    produto.Preco);

                pedido.AdicionarItem(item);

                await _produtoRepository.AtualizarAsync(produto);
            }

            pedido.ValidarPedido();

            await _pedidoRepository.AdicionarAsync(pedido);

            await RegistrarHistoricoAsync(pedido.Id, StatusPedido.Criado, StatusPedido.Criado, "Pedido criado");

            return pedido.Id;
        }

        public async Task<PedidoDto> ObterPorIdAsync(Guid id)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(id);

            if (pedido is null)
                throw new DomainException("Pedido não encontrado.");

            return MapearPedido(pedido);
        }

        public async Task<IEnumerable<PedidoDto>> ObterTodosAsync()
        {
            var pedidos = await _pedidoRepository.ObterTodosAsync();

            return pedidos
                 .Select(MapearPedido)
                 .ToList();
        }

        public async Task MarcarComoPagoAsync(Guid pedidoId)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(pedidoId);

            if (pedido is null)
                throw new DomainException("Pedido não encontrado.");

            var statusAnterior = pedido.Status;

            pedido.MarcarComoPago();

            await _pedidoRepository.AtualizarAsync(pedido);

            await RegistrarHistoricoAsync(pedido.Id, statusAnterior, pedido.Status);
        }

        public async Task MarcarComoEnviadoAsync(Guid pedidoId)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(pedidoId);

            if (pedido is null)
                throw new DomainException("Pedido não encontrado.");

            var statusAnterior = pedido.Status;

            pedido.MarcarComoEnviado();

            await _pedidoRepository.AtualizarAsync(pedido);

            await RegistrarHistoricoAsync(pedido.Id, statusAnterior, pedido.Status);
        }

        public async Task CancelarAsync(Guid pedidoId, string? motivo)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(pedidoId);

            if (pedido is null)
                throw new DomainException("Pedido não encontrado.");

            if (pedido.Status == StatusPedido.Cancelado)
                throw new DomainException("O pedido já está cancelado.");

            foreach (var item in pedido.Itens)
            {
                var produto = await _produtoRepository.ObterPorIdAsync(item.ProdutoId);

                if (produto is null)
                    throw new DomainException($"Produto {item.ProdutoId} não encontrado.");

                produto.ReporEstoque(item.Quantidade);

                await _produtoRepository.AtualizarAsync(produto);
            }

            var statusAnterior = pedido.Status;

            pedido.Cancelar(motivo);

            await _pedidoRepository.AtualizarAsync(pedido);

            await RegistrarHistoricoAsync(pedido.Id, statusAnterior, pedido.Status, motivo);
        }

        private static PedidoDto MapearPedido(Pedido pedido)
        {
            return new PedidoDto
            {
                Id = pedido.Id,
                ClienteId = pedido.ClienteId,
                Status = pedido.Status,
                ValorTotal = pedido.ValorTotal,
                CreatedAt = pedido.CreatedAt,

                Itens = pedido.Itens.Select(item => new PedidoItemDto
                {
                    ProdutoId = item.ProdutoId,
                    NomeProduto = item.Produto?.Nome ?? string.Empty,
                    Quantidade = item.Quantidade,
                    PrecoUnitario = item.PrecoUnitario,
                    ValorTotal = item.ValorTotal
                }).ToList()
            };
        }

        private async Task RegistrarHistoricoAsync(Guid pedidoId, StatusPedido anterior, StatusPedido novo, string? motivo = null)
        {
            var historico = new HistoricoStatusPedido(
                pedidoId,
                anterior,
                novo,
                motivo);

            await _historicoRepository.AdicionarAsync(historico);
        }
    }
}
