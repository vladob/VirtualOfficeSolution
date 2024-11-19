using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataAccess.Entities;

namespace DataAccess.Configurations
{
    public class ExportItemConfiguration : IEntityTypeConfiguration<ExportItem>
    {
        public void Configure(EntityTypeBuilder<ExportItem> builder)
        {
            builder.ToTable("Items");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasMaxLength(255).IsRequired(false);
            builder.Property(e => e.ItemType).HasMaxLength(50).IsRequired(false);
            builder.Property(e => e.Unit).HasMaxLength(20).IsRequired(false);
        }
    }
}
