using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataAccess.Entities;

namespace DataAccess.Configurations
{
    public class CustomVatInterfaceConfiguration : IEntityTypeConfiguration<CustomVatInterface>
    {
        public void Configure(EntityTypeBuilder<CustomVatInterface> builder)
        {
            builder.ToTable("VatSummaries");

            builder.HasKey(v => v.Id);
            builder.Property(v => v.TaxBase).IsRequired();
            builder.Property(v => v.VatAmount).IsRequired();

        }
    }
}
