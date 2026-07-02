using OrderManagement.Domain.Common;
using OrderManagement.Domain.Exceptions;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Domain.Entities
{
    public class Cliente : BaseEntity
    {
        // --- Regras ---
        // - As propriedades serão privadas para garantir que apenas métodos da própria classe possam alterá-las.
        // - A criação será feita através do construtor, que receberá os parâmetros obrigatórios.
        // - Para evitar estados inválidos

        public string Nome { get; private set; } = string.Empty;

        public Email Email { get; private set; }

        public Documento Documento { get; private set; }

        public bool Ativo { get; private set; }

        private Cliente() { } // para o EF Core

        public IReadOnlyCollection<Pedido> Pedidos => _pedidos.AsReadOnly();

        private readonly List<Pedido> _pedidos = new();

        public Cliente(string nome, Email email, Documento documento)
        {
            AlterarNome(nome);
            AlterarEmail(email);
            AlterarDocumento(documento);

            Ativo = true;
        }

        public void AlterarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new DomainException("O nome do cliente é obrigatório.");
            
            Nome = nome.Trim();
            SetUpdatedAt();
        }

        public void AlterarEmail(Email email)
        {
            Email = email ?? throw new DomainException("O email do cliente é obrigatório.");
            SetUpdatedAt();
        }

        public void AlterarDocumento(Documento documento)
        {
            Documento = documento ?? throw new DomainException("O documento do cliente é obrigatório.");
            SetUpdatedAt();
        }

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

        public bool PodeCriarPedido()
        {
            return Ativo;
        }
    }
}
