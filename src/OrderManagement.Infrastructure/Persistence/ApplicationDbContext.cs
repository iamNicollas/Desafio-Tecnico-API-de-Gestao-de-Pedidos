using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        // Cada DbSet representa uma tabela no banco de dados.
        // O Entity Framework utilizará essas propriedades para realizar
        // consultas, inserções, atualizações e remoções.
        public DbSet<Cliente> Clientes => Set<Cliente>();

        public DbSet<Produto> Produtos => Set<Produto>();

        public DbSet<Pedido> Pedidos => Set<Pedido>();

        public DbSet<PedidoItem> PedidoItens => Set<PedidoItem>();

        public DbSet<HistoricoStatusPedido> HistoricoStatusPedido => Set<HistoricoStatusPedido>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
