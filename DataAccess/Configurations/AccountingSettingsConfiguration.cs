using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataAccess.Entities;

namespace DataAccess.Configurations
{
    public class AccountingSettingsConfiguration : IEntityTypeConfiguration<AccountingSettingsElement>
    {
        public void Configure(EntityTypeBuilder<AccountingSettingsElement> builder)
        {
            builder.ToTable("AccountingSettings");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.AccountingItem_Value).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.AccountingItem_Code).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.AccountingItem_ResourceType).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.NumericCode_Value).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.NumericCode_Name).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.NumericCode_PaymentType).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.CashRegister_Value).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.CashRegister_Name).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.Project_Value).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.Project_Code).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.Order_Value).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.Order_Code).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.Order_Name).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.Activity_Value).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.Activity_Code).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.ExpenditureCenter_Value).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.ExpenditureCenter_Code).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.GeneralDocumentAgenda_Name).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.GeneralDocumentAgenda_Code).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.AccountingSoftwareAgenda_Name).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.AccountingSoftwareAgenda_Code).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.PredefinedNote_Value).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.ClassificationVat_Name).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.ClassificationVat_Code).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.ClassificationVat_ValidFrom).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.ClassificationVat_ValidTo).HasMaxLength(100).IsRequired(false);
        }
    }
}
