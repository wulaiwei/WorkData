using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WorkData.EF.Domain.Entity;

namespace WorkData.EF.Domain.Mapping
{
    public class PrivilegeMap : EntityTypeConfiguration<Privilege>
    {
        public PrivilegeMap()
        {
            this.ToTable("EF_PRIVILEGE");

            // Primary Key
            this.HasKey(t => t.PrivilegeId);
            this.Property(t => t.PrivilegeId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.PrivilegeId).HasColumnName("PRIVILEGE_ID");
            this.Property(t => t.ResourceId).HasColumnName("RESOURCE_ID");
            this.Property(t => t.OperationId).HasColumnName("OPERATION_ID");

            this.HasRequired(t => t.Operation)
                .WithMany(t => t.Privileges)
                .HasForeignKey(d => d.OperationId)
                //.WillCascadeOnDelete(true)//¼¶Áª
                ;

            this.HasRequired(t => t.Resource)
                .WithMany(t => t.Privileges)
                .HasForeignKey(d => d.ResourceId)
                //.WillCascadeOnDelete(true)
                ;
        }
    }
}