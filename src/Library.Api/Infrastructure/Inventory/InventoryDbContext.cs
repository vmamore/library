namespace Library.Api.Infrastructure.Inventory
{
    using Library.Api.Domain.Inventory;
    using Library.Api.Infrastructure.Inventory.Configurations;
    using Microsoft.EntityFrameworkCore;

    public class InventoryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
