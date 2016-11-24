using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WorkData.EF.Domain.Entity;

namespace WorkData.EF.Domain.Mapping
{
    public class ModelMap : EntityTypeConfiguration<Model>
    {
        public ModelMap()
        {
            // Primary Key
            this.HasKey(t => t.ModelId);

            // Properties
            this.Property(t => t.ModelId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("EF_MODEL");
            this.Property(t => t.ModelId).HasColumnName("MODEL_ID");
            this.Property(t => t.Name).HasColumnName("NAME");
            this.Property(t => t.Code).HasColumnName("CODE");
            this.Property(t => t.Status).HasColumnName("STATUS");
            this.Property(t => t.Description).HasColumnName("DESCRIPTION");

            this.HasMany(p => p.ModelFields)
                .WithMany(c => c.Models)
                .Map(manyToMany => manyToMany
                  .ToTable("EF_MODEL_MODEL_FIELD", "dbo")
                  .MapLeftKey("MODEL_ID")
                  .MapRightKey("MODEL_FIELD_ID"));
        }
    }
}
