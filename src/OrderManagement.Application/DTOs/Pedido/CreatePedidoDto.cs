namespace OrderManagement.Application.DTOs.Pedido
{
    public class CreatePedidoDto
    {
        public Guid ClienteId { get; set; }

        public List<CreatePedidoItemDto> Itens { get; set; } = [];
    }
}
