namespace OrderManagement.Application.DTOs.Produto
{
    public class UpdateProdutoDto
    {
        public string Nome { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public decimal Preco { get; set; }

        public int Estoque { get; set; }
    }
}
