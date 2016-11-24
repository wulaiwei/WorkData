using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WorkData.EF.Domain.Entity;

namespace WorkData.EF.Domain.Mapping
{
    public class ContentStringFieldMap : EntityTypeConfiguration<ContentStringField>
    {
        public ContentStringFieldMap()
        {
            // Primary Key
            this.HasKey(t => t.ContentStringFieldId);

            // Properties
            this.Property(t => t.ContentStringFieldId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.FieldCode)
                .HasMaxLength(50);

            this.Property(t => t.FieldValue)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("EF_CONTENT_STRING_FIELD");
            this.Property(t => t.ContentStringFieldId).HasColumnName("CONTENT_STRING_FIELD_ID");
            this.Property(t => t.ContentId).HasColumnName("CONTENT_ID");
            this.Property(t => t.FieldCode).HasColumnName("FIELD_CODE");
            this.Property(t => t.FieldValue).HasColumnName("FIELD_VALUE");

            // Relationships
            this.HasOptional(t => t.Content)
                .WithMany(t => t.ContentStringFields)
                .HasForeignKey(d => d.ContentId)
                .WillCascadeOnDelete(true);

        }
    }
}
