using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataAccess.Entities;

namespace DataAccess.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.StreetName).HasMaxLength(255).IsRequired(false);
            builder.Property(a => a.PostalCode).HasMaxLength(20).IsRequired(false);
            builder.Property(a => a.PropertyRegistrationNumber).HasMaxLength(20).IsRequired(false);
            builder.Property(a => a.BuildingNumber).HasMaxLength(20).IsRequired(false);
            builder.Property(a => a.Municipality).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.Country).HasMaxLength(100).IsRequired(false);

            // Add other field constraints as needed
        }
    }
}
