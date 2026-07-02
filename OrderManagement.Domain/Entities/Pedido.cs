using OrderManagement.Domain.Common;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Domain.Entities;

public class Pedido : BaseEntity
{
    private readonly List<PedidoItem> _itens = new();

    private readonly List<HistoricoStatusPedido> _historico = new();

    public Guid ClienteId { get; private set; }

    public StatusPedido Status { get; private set; }

    public decimal ValorTotal { get; private set; }

    public IReadOnlyCollection<PedidoItem> Itens => _itens.AsReadOnly();

    public IReadOnlyCollection<HistoricoStatusPedido> Historico => _historico.AsReadOnly();

    // Navegação (EF Core)
    public Cliente? Cliente { get; private set; }

    private Pedido()
    {
    }

    public Pedido(Guid clienteId)
    {
        if (clienteId == Guid.Empty)
            throw new DomainException("Cliente inválido.");

        ClienteId = clienteId;
        Status = StatusPedido.Criado;
    }

    public void AdicionarItem(PedidoItem item)
    {
        if (item is null)
            throw new DomainException("Item inválido.");

        _itens.Add(item);

        CalcularValorTotal();

        SetUpdatedAt();
    }

    public void ValidarPedido()
    {
        if (!_itens.Any())
            throw new DomainException("O pedido deve possuir pelo menos um item.");
    }

    public void MarcarComoPago()
    {
        AlterarStatus(StatusPedido.Pago);
    }

    public void MarcarComoEnviado()
    {
        AlterarStatus(StatusPedido.Enviado);
    }

    public void Cancelar(string? motivo = null)
    {
        AlterarStatus(StatusPedido.Cancelado, motivo);
    }

    private void AlterarStatus(StatusPedido novoStatus, string? motivo = null)
    {
        if (Status == novoStatus)
            throw new DomainException("O pedido já está neste status.");

        if (!TransicaoValida(Status, novoStatus))
            throw new DomainException("Transição de status inválida.");

        var statusAnterior = Status;

        Status = novoStatus;

        SetUpdatedAt();
    }

    private void CalcularValorTotal()
    {
        ValorTotal = _itens.Sum(i => i.ValorTotal);
    }

    private static bool TransicaoValida(StatusPedido atual, StatusPedido novo)
    {
        return (atual, novo) switch
        {
            (StatusPedido.Criado, StatusPedido.Pago) => true,

            (StatusPedido.Pago, StatusPedido.Enviado) => true,

            (StatusPedido.Criado, StatusPedido.Cancelado) => true,

            _ => false
        };
    }
}