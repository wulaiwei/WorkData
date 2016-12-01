using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WorkData.EF.Domain.Entity;

namespace WorkData.EF.Domain.Mapping
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            // Primary Key
            this.HasKey(t => t.RoleId);

            this.Property(t => t.RoleId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                //.IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Code)
                //.IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("EF_ROLE");
            this.Property(t => t.RoleId).HasColumnName("ROLE_ID");
            this.Property(t => t.Name).HasColumnName("NAME");
            this.Property(t => t.Code).HasColumnName("CODE");
            this.Property(t => t.Status).HasColumnName("STATUS");

            #region MyRegion
            // Relationships
            //this.HasMany(t => t.Resources)
            //    .WithMany(t => t.Roles)
            //    .Map(m =>
            //    {
            //        m.ToTable("EF_ROLE_PRIVILEGE");
            //        m.MapLeftKey("ROLE_ID");
            //        m.MapRightKey("PRIVILEGE_ID");
            //    })
            //    ; 
            #endregion

            // Relationships
            this.HasMany(t => t.Resources)
                .WithMany(t => t.Roles)
                .Map(m =>
                {
                    m.ToTable("EF_ROLE_RESOURCE");
                    m.MapLeftKey("ROLE_ID");
                    m.MapRightKey("RESOURCE_ID");
                })
                ;

        }
    }
}