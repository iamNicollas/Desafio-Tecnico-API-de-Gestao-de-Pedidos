using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Infrastructure.Persistence.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Descricao)
                .HasMaxLength(500);

            // Define a precisão da coluna decimal no SQL Server,
            // evitando perda de precisão em valores monetários.
            builder.Property(p => p.Preco)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(p => p.Estoque)
                .IsRequired();

            builder.Property(p => p.Ativo)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.UpdatedAt);
        }
    }
}
