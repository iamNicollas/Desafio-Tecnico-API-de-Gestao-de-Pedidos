namespace OrderManagement.Application.DTOs.Pedido
{
    public class PedidoItemDto
    {
        public Guid ProdutoId { get; set; }

        public string NomeProduto { get; set; } = string.Empty;

        public int Quantidade { get; set; }

        public decimal PrecoUnitario { get; set; }

        public decimal ValorTotal { get; set; }
    }
}
