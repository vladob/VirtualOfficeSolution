using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Documents");

            builder.HasKey(d => d.Id);
            builder.Property(d => d.ScanedForCompany).HasMaxLength(20).IsRequired(false);
            builder.Property(d => d.DocumentErpId).HasMaxLength(100).IsRequired(false);
            builder.Property(d => d.ReceiptUID).HasMaxLength(100).IsRequired(false);
            builder.Property(d => d.Currency).HasMaxLength(20).IsRequired(false);
            builder.Property(d => d.AccountingCode).HasMaxLength(20).IsRequired(false);
            builder.Property(d => d.OrganizationId).HasMaxLength(100).IsRequired(false);
            builder.Property(d => d.OrganizationVAT).HasMaxLength(100).IsRequired(false);
            builder.Property(d => d.OrganizationVatId).HasMaxLength(100).IsRequired(false);
            builder.Property(d => d.OrganizationTaxId).HasMaxLength(100).IsRequired(false);
            builder.Property(d => d.OrganizationName).HasMaxLength(255).IsRequired(false);
            builder.Property(d => d.StreetName).HasMaxLength(255).IsRequired(false);
            builder.Property(d => d.Municipality).HasMaxLength(255).IsRequired(false);
            builder.Property(d => d.PostalCode).HasMaxLength(20).IsRequired(false);
            builder.Property(d => d.Email).HasMaxLength(255).IsRequired(false);
            builder.Property(d => d.PaymentType).HasMaxLength(20).IsRequired(false);
            builder.Property(d => d.InvoiceNumber).HasMaxLength(20).IsRequired(false);

            builder.HasOne(d => d.Address)
                    .WithOne()
                    .HasForeignKey<Address>(a => a.DocumentId)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(d => d.Items)
                    .WithOne()
                    .HasForeignKey(i => i.DocumentId)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(d => d.VatSummary)
                    .WithOne()
                    .HasForeignKey(v => v.DocumentId)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

