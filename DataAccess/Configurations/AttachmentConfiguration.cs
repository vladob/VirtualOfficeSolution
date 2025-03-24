using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.ToTable("Attachments");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.DocumentId).HasMaxLength(50).IsRequired(false);
            builder.Property(a => a.DownloadUrl).HasMaxLength(255).IsRequired(false);
            builder.Property(a => a.FileName).HasMaxLength(255).IsRequired(false);
            builder.Property(a => a.FileType).HasMaxLength(50).IsRequired(false);
        }
    }
}
