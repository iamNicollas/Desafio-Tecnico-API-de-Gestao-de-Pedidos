namespace OrderManagement.Application.DTOs.Pedido
{
    public class CreatePedidoItemDto
    {
        public Guid ProdutoId { get; set; }

        public int Quantidade { get; set; }
    }
}
