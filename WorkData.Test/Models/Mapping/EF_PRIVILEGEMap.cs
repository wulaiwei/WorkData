using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WorkData.Test.Models.Mapping
{
    public class EF_PRIVILEGEMap : EntityTypeConfiguration<EF_PRIVILEGE>
    {
        public EF_PRIVILEGEMap()
        {
            // Primary Key
            this.HasKey(t => t.PRIVILEGE_ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("EF_PRIVILEGE");
            this.Property(t => t.PRIVILEGE_ID).HasColumnName("PRIVILEGE_ID");
            this.Property(t => t.RESOURCE_ID).HasColumnName("RESOURCE_ID");
            this.Property(t => t.OPERATION_ID).HasColumnName("OPERATION_ID");

            // Relationships
            this.HasMany(t => t.EF_ROLE)
                .WithMany(t => t.EF_PRIVILEGE)
                .Map(m =>
                    {
                        m.ToTable("EF_ROLE_PRIVILEGE");
                        m.MapLeftKey("PRIVILEGE_ID");
                        m.MapRightKey("ROLE_ID");
                    });

            this.HasRequired(t => t.EF_OPERATION)
                .WithMany(t => t.EF_PRIVILEGE)
                .HasForeignKey(d => d.OPERATION_ID);
            this.HasRequired(t => t.EF_RESOURCE)
                .WithMany(t => t.EF_PRIVILEGE)
                .HasForeignKey(d => d.RESOURCE_ID);

        }
    }
}
