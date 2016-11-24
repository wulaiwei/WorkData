using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WorkData.EF.Domain.Entity;

namespace WorkData.EF.Domain.Mapping
{
    public class ContentTimeFieldMap : EntityTypeConfiguration<ContentTimeField>
    {
        public ContentTimeFieldMap()
        {
            // Primary Key
            this.HasKey(t => t.ContentTimeFieldId);

            // Properties
            this.Property(t => t.ContentTimeFieldId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.FieldCode)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("EF_CONTENT_TIME_FIELD");
            this.Property(t => t.ContentTimeFieldId).HasColumnName("CONTENT_TIME_FIELD_ID");
            this.Property(t => t.ContentId).HasColumnName("CONTENT_ID");
            this.Property(t => t.FieldCode).HasColumnName("FIELD_CODE");
            this.Property(t => t.FieldValue).HasColumnName("FIELD_VALUE");

            // Relationships
            this.HasOptional(t => t.Content)
                .WithMany(t => t.ContentTimeFields)
                .HasForeignKey(d => d.ContentId)
                .WillCascadeOnDelete(true);

        }
    }
}
