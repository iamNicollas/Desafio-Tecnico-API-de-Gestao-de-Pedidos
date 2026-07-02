using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Infrastructure.Persistence.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(150);

            // Email é um Value Object. O EF armazenará apenas o seu valor.
            // HasConversion diz ao EF - Quando salvar no banco, grave apenas a string.
            // Quando ler do banco, recrie automaticamente o Value Object.
            builder.Property(c => c.Email)
                .HasConversion(
                    email => email.Endereco,
                    valor => new Domain.ValueObjects.Email(valor))
                .HasMaxLength(150)
                .IsRequired();

            // Documento também é um Value Object.
            builder.Property(c => c.Documento)
                .HasConversion(
                    documento => documento.Numero,
                    valor => new Domain.ValueObjects.Documento(valor))
                .HasMaxLength(18)
                .IsRequired();

            builder.Property(c => c.Ativo)
                .IsRequired();

            builder.Property(c => c.CreatedAt)
                .IsRequired();

            builder.Property(c => c.UpdatedAt);

            // Garante que não existam dois clientes com o mesmo CPF/CNPJ.
            builder.HasIndex(c => c.Documento)
                .IsUnique();
        }
    }
}
