using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WorkData.EF.Domain.Entity;

namespace WorkData.EF.Domain.Mapping
{
    public class ContentMap : EntityTypeConfiguration<Content>
    {
        public ContentMap()
        {
            // Primary Key
            this.HasKey(t => t.ContentId);

            // Properties
            this.Property(t => t.ContentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("EF_CONTENT");
            this.Property(t => t.ContentId).HasColumnName("CONTENT_ID");
            this.Property(t => t.ModelId).HasColumnName("MODEL_ID");
            this.Property(t => t.CategoryId).HasColumnName("CATEGORY_ID");

            // Relationships
            this.HasOptional(t => t.Model)
                .WithMany(t => t.Contents)
                .HasForeignKey(d => d.ModelId);

            // Relationships
            this.HasOptional(t => t.Category)
                .WithMany(t => t.Contents)
                .HasForeignKey(d => d.CategoryId);

        }
    }
}
