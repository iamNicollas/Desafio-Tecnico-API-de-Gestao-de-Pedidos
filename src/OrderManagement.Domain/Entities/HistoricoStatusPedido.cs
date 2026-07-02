using OrderManagement.Domain.Common;
using OrderManagement.Domain.Enums;

namespace OrderManagement.Domain.Entities;

public class HistoricoStatusPedido : BaseEntity
{
    public Guid PedidoId { get; private set; }

    public StatusPedido StatusAnterior { get; private set; }

    public StatusPedido NovoStatus { get; private set; }

    public string? Motivo { get; private set; }

    public DateTime DataAlteracao { get; private set; }

    // Navegação (EF Core)
    public Pedido? Pedido { get; private set; }

    private HistoricoStatusPedido() { }

    public HistoricoStatusPedido(Guid pedidoId, StatusPedido statusAnterior, StatusPedido novoStatus, string? motivo = null)
    {
        PedidoId = pedidoId;
        StatusAnterior = statusAnterior;
        NovoStatus = novoStatus;
        Motivo = motivo;
        DataAlteracao = DateTime.UtcNow;
    }
}