using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Infrastructure.Persistence.Configurations
{
    public class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.ToTable("PedidoItens");

            builder.HasKey(pi => pi.Id);

            builder.Property(pi => pi.Quantidade)
                .IsRequired();

            // Define a precisão do valor unitário no banco.
            builder.Property(pi => pi.PrecoUnitario)
                .HasPrecision(18, 2)
                .IsRequired();

            // Valor calculado da quantidade × preço unitário.
            builder.Property(pi => pi.ValorTotal)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(pi => pi.CreatedAt)
                .IsRequired();

            builder.Property(pi => pi.UpdatedAt);

            // Um Produto pode estar presente em vários itens de pedido.
            // Cada item referencia um único produto.
            builder.HasOne(pi => pi.Produto)
                .WithMany(p => p.PedidoItens)
                .HasForeignKey(pi => pi.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict); // Impede que um produto seja excluído caso ele já tenha sido utilizado em algum pedido.

            // O relacionamento com Pedido já foi configurado em PedidoConfiguration.
        }
    }
}
