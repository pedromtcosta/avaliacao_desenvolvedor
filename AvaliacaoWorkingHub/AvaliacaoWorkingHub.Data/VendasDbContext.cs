using AvaliacaoWorkingHub.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AvaliacaoWorkingHub.Data
{
    public class VendasDbContext : DbContext
    {
        public VendasDbContext() : base("Vendas") { }

        public virtual DbSet<Venda> Vendas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
