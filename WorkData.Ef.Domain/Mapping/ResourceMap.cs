using EFModel.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WorkData.EF.Domain.Mapping
{
    public class ResourceMap : EntityTypeConfiguration<Resource>
    {
        public ResourceMap()
        {


            // Table & Column Mappings
            this.ToTable("EF_RESOURCE");
            this.Property(t => t.ResourceId).HasColumnName("RESOURCE_ID");
            this.Property(t => t.ParentId).HasColumnName("PARENT_ID");
            this.Property(t => t.ResourceName).HasColumnName("RESOURCE_NAME");
            this.Property(t => t.ResourceUrl).HasColumnName("RESOURCE_URL").IsOptional();
            this.Property(t => t.Layer).HasColumnName("LAYER");
            this.Property(t => t.IsLock).HasColumnName("IS_LOCK");
            this.Property(t => t.ResourceImg).HasColumnName("RESOURCE_IMG").IsOptional();
            this.Property(t => t.Sort).HasColumnName("SORT");
            this.Property(t => t.HasLevel).HasColumnName("HAS_LEVEL");

            // Primary Key
            this.HasKey(t => t.ResourceId);

            //主键自增长
            this.Property(t => t.ResourceId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Properties
            this.Property(t => t.ResourceName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ResourceUrl)
                .HasMaxLength(200);

            this.Property(t => t.ResourceImg)
                .HasMaxLength(200);

            // Ignore
            this.Ignore(t => t.OperationList);
        }
    }
}