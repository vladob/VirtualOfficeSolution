using Microsoft.EntityFrameworkCore;
using DataAccess.Entities;
using Microsoft.Extensions.Options;

namespace DataAccess
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AccountingSettingsElement> AccountingSettings { get; set; }
        public DbSet<ExportItem> Items { get; set; }
        public DbSet<CustomVatInterface> VatSummarys { get; set; }
        public DbSet<ItemsAccountingSettingsElement> ItemsAccountingSettings { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Automatically apply all configurations in the current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // Call the base class implementation
            base.OnModelCreating(modelBuilder);
        }
    }
}
