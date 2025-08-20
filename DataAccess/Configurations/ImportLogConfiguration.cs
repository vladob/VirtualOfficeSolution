using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class ImportLogConfiguration : IEntityTypeConfiguration<ImportLog>
{
    public void Configure(EntityTypeBuilder<ImportLog> b)
    {
        b.ToTable("ImportLog", "dbo");
        b.HasKey(x => x.Id).HasName("PK_dbo_ImportLog");

        b.Property(x => x.CompanyCin).IsRequired().HasMaxLength(20);
        b.Property(x => x.ExecutionDate).HasPrecision(3);
        // other ints default conventions are fine
        b.HasIndex(x => new { x.CompanyCin, x.ExecutionDate }).HasDatabaseName("IX_ImportLog_CompanyCin_ExecutionDate");
    }
}
