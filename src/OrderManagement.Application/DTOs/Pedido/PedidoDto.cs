using OrderManagement.Domain.Enums;

namespace OrderManagement.Application.DTOs.Pedido
{
    public class PedidoDto
    {
        public Guid Id { get; set; }

        public Guid ClienteId { get; set; }

        public StatusPedido Status { get; set; }

        public decimal ValorTotal { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<PedidoItemDto> Itens { get; set; } = [];
    }
}
