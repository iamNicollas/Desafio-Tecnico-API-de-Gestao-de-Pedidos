namespace OrderManagement.Application.DTOs.Produto
{
    public class CreateProdutoDto
    {
        public string Nome { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public decimal Preco { get; set; }

        public int Estoque { get; set; }
    }
}
