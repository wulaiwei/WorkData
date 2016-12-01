using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WorkData.Test.Models.Mapping
{
    public class EF_RESOURCEMap : EntityTypeConfiguration<EF_RESOURCE>
    {
        public EF_RESOURCEMap()
        {
            // Primary Key
            this.HasKey(t => t.RESOURCE_ID);

            // Properties
            this.Property(t => t.RESOURCE_NAME)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.RESOURCE_URL)
                .HasMaxLength(200);

            this.Property(t => t.RESOURCE_IMG)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("EF_RESOURCE");
            this.Property(t => t.RESOURCE_ID).HasColumnName("RESOURCE_ID");
            this.Property(t => t.PARENT_ID).HasColumnName("PARENT_ID");
            this.Property(t => t.RESOURCE_NAME).HasColumnName("RESOURCE_NAME");
            this.Property(t => t.RESOURCE_URL).HasColumnName("RESOURCE_URL");
            this.Property(t => t.LAYER).HasColumnName("LAYER");
            this.Property(t => t.IS_LOCK).HasColumnName("IS_LOCK");
            this.Property(t => t.RESOURCE_IMG).HasColumnName("RESOURCE_IMG");
            this.Property(t => t.SORT).HasColumnName("SORT");
            this.Property(t => t.HAS_LEVEL).HasColumnName("HAS_LEVEL");
        }
    }
}
