using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WorkData.EF.Domain.Entity;

namespace WorkData.EF.Domain.Mapping
{
    public class ContentDoubleFieldMap : EntityTypeConfiguration<ContentDoubleField>
    {
        public ContentDoubleFieldMap()
        {
            // Primary Key
            this.HasKey(t => t.ContentDoubleFieldId);

            // Properties
            this.Property(t => t.ContentDoubleFieldId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.FieldCode)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("EF_CONTENT_DOUBLE_FIELD");
            this.Property(t => t.ContentDoubleFieldId).HasColumnName("CONTENT_DOUBLE_FIELD_ID");
            this.Property(t => t.ContentId).HasColumnName("CONTENT_ID");
            this.Property(t => t.FieldCode).HasColumnName("FIELD_CODE");
            this.Property(t => t.FieldValue).HasColumnName("FIELD_VALUE");

            // Relationships
            this.HasOptional(t => t.Content)
                .WithMany(t => t.ContentDoubleFields)
                .HasForeignKey(d => d.ContentId)
                .WillCascadeOnDelete(true);

        }
    }
}
