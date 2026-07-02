using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Infrastructure.Persistence.Configuration
{
    public class HistoricoStatusPedidoConfiguration : IEntityTypeConfiguration<HistoricoStatusPedido>
    {
        public void Configure(EntityTypeBuilder<HistoricoStatusPedido> builder)
        {
            builder.ToTable("HistoricoStatusPedidos");

            builder.HasKey(h => h.Id);

            // Os enums serão armazenados como inteiros no banco.
            builder.Property(h => h.StatusAnterior)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(h => h.NovoStatus)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(h => h.Motivo)
                .HasMaxLength(500);

            builder.Property(h => h.DataAlteracao)
                .IsRequired();

            builder.Property(h => h.CreatedAt)
                .IsRequired();

            builder.Property(h => h.UpdatedAt);

            // O relacionamento com Pedido já foi configurado em PedidoConfiguration.
        }
    }
}
