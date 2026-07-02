using OrderManagement.Domain.Common;
using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Domain.Entities
{
    public class Produto : BaseEntity
    {
        // --- Regras ---
        // - As propriedades serão privadas para garantir que apenas métodos da própria classe possam alterá-las.
        // - A criação será feita através do construtor, que receberá os parâmetros obrigatórios.
        // - Para evitar estados inválidos

        public string Nome { get; private set; } = string.Empty;

        public string Descricao { get; private set; } = string.Empty;

        public decimal Preco { get; private set; }

        public int Estoque { get; private set; }

        public bool Ativo { get; private set; }

        private Produto() { } // para o EF Core

        private readonly List<PedidoItem> _pedidoItens = new();

        public IReadOnlyCollection<PedidoItem> PedidoItens => _pedidoItens.AsReadOnly();

        public Produto(string nome, string descricao, decimal preco, int estoque)
        {
            AlterarNome(nome);
            AlterarDescricao(descricao);
            AlterarPreco(preco);
            DefinirEstoque(estoque);
            
            Ativo = true;
        }

        public void AlterarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new DomainException("O nome do produto é obrigatório.");
           
            Nome = nome.Trim();
            SetUpdatedAt();
        }

        public void AlterarDescricao(string descricao)
        {
            Descricao = descricao?.Trim() ?? string.Empty;
            SetUpdatedAt();
        }

        public void AlterarPreco(decimal preco)
        {
            if (preco <= 0)
                throw new DomainException("O preço deve ser maior que zero.");

            Preco = preco;
            SetUpdatedAt();
        }

        public void DefinirEstoque(int quantidade)
        {
            if (quantidade < 0)
                throw new DomainException("O estoque não pode ser negativo.");
           
            Estoque = quantidade;
            SetUpdatedAt();
        }

        public bool PossuiEstoque(int quantidade)
        {
            if(quantidade <= 0)
                throw new DomainException("A quantidade deve ser maior que zero.");

            return Estoque >= quantidade;
        }
        
        #region Movimentação de Estoque
        public void DebitarEstoque(int quantidade)
        {
            if(!PossuiEstoque(quantidade))
                throw new DomainException("Estoque insuficiente.");

            Estoque -= quantidade;
            SetUpdatedAt();
        }

        public void ReporEstoque(int quantidade)
        {
            if(quantidade <= 0)
                throw new DomainException("A quantidade deve ser maior que zero.");

            Estoque += quantidade;
            SetUpdatedAt();
        }
        #endregion

        public void Ativar()
        {
            Ativo = true;
            SetUpdatedAt();
        }

        public void Desativar()
        {
            Ativo = false;
            SetUpdatedAt();
        }
    }
}
