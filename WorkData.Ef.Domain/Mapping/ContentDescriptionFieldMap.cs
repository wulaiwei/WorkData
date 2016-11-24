using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WorkData.EF.Domain.Entity;

namespace WorkData.EF.Domain.Mapping
{
    public class ContentDescriptionFieldMap : EntityTypeConfiguration<ContentDescriptionField>
    {
        public ContentDescriptionFieldMap()
        {
            // Primary Key
            this.HasKey(t => t.ContentDescriptionFieldId);

            // Properties
            this.Property(t => t.ContentDescriptionFieldId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.FieldCode)
                .HasMaxLength(50);

            this.Property(t => t.FieldValue)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("EF_CONTENT_DESCRIPTION_FIELD");
            this.Property(t => t.ContentDescriptionFieldId).HasColumnName("CONTENT_DESCRIPTION_FIELD_ID");
            this.Property(t => t.ContentId).HasColumnName("CONTENT_ID");
            this.Property(t => t.FieldCode).HasColumnName("FIELD_CODE");
            this.Property(t => t.FieldValue).HasColumnName("FIELD_VALUE");

            // Relationships
            this.HasOptional(t => t.Content)
                .WithMany(t => t.ContentDescriptionFields)
                .HasForeignKey(d => d.ContentId)
                .WillCascadeOnDelete(true);

        }
    }
}
