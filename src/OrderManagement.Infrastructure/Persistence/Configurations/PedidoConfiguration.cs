using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Infrastructure.Persistence.Configurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");

            builder.HasKey(p => p.Id);

            // O StatusPedido é um enum. O EF irá armazená-lo como inteiro.
            builder.Property(p => p.Status)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(p => p.ValorTotal)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.UpdatedAt);

            // Um Cliente pode possuir vários Pedidos.
            // Um Pedido pertence a um único Cliente.
            builder.HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Restrict); // Não será possível excluir um cliente que possua pedidos.

            // Um Pedido possui vários Itens.
            // Cada Item pertence a um único Pedido.
            builder.HasMany(p => p.Itens)
                .WithOne(i => i.Pedido)
                .HasForeignKey(i => i.PedidoId)
                .OnDelete(DeleteBehavior.Cascade); // Se um pedido for excluído, seus itens também serão.

            // Um Pedido possui um histórico de alterações de status.
            builder.HasMany(p => p.Historico)
                .WithOne(h => h.Pedido)
                .HasForeignKey(h => h.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
