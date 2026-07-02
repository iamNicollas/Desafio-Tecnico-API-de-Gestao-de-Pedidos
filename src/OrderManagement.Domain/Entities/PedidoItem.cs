using OrderManagement.Domain.Common;
using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Domain.Entities
{
    public class PedidoItem : BaseEntity
    {
        // --- Regras ---
        // - As propriedades serão privadas para garantir que apenas métodos da própria classe possam alterá-las.
        // - A criação será feita através do construtor, que receberá os parâmetros obrigatórios.
        // - Para evitar estados inválidos

        public Guid PedidoId { get; private set; }

        public Guid ProdutoId { get; private set; }

        public int Quantidade { get; private set; }

        public decimal PrecoUnitario { get; private set; }

        public decimal ValorTotal { get; private set; }

        // Navegação (EF Core)
        public Pedido? Pedido { get; private set; }
        public Produto? Produto { get; private set; }

        private PedidoItem() { }

        public PedidoItem(Guid produtoId, int quantidade, decimal precoUnitario)
        {
            if(produtoId == Guid.Empty)
                throw new DomainException("Produto inválido.");

            AlterarQuantidade(quantidade);
            DefinirPrecoUnitario(precoUnitario);

            ProdutoId = produtoId;
            CalcularValorTotal();
        }

        public void AlterarQuantidade(int quantidade)
        {
            if (quantidade <= 0)
                throw new DomainException("A quantidade deve ser maior que zero.");
           
            Quantidade = quantidade;
            CalcularValorTotal();
            SetUpdatedAt();
        }

        public void DefinirPrecoUnitario(decimal precoUnitario)
        {
            if (precoUnitario <= 0)
                throw new DomainException("O preço unitário deve ser maior que zero.");
           
            PrecoUnitario = precoUnitario;
            CalcularValorTotal();
            SetUpdatedAt();
        }

        private void CalcularValorTotal()
        {
            ValorTotal = Quantidade * PrecoUnitario;
        }
    }
}
