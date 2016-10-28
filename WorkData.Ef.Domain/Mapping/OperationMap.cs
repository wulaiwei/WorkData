using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WorkData.EF.Domain.Entity;

namespace WorkData.EF.Domain.Mapping
{
    public class OperationMap : EntityTypeConfiguration<Operation>
    {
        public OperationMap()
        {

            // Table & Column Mappings
            this.ToTable("EF_OPERATION");
            this.Property(t => t.OperationId).HasColumnName("OPERATION_ID");
            this.Property(t => t.Name).HasColumnName("NAME");
            this.Property(t => t.Code).HasColumnName("CODE");
            this.Property(t => t.Status).HasColumnName("STATUS");

            // Primary Key
            this.HasKey(t => t.OperationId);

            this.Property(t => t.OperationId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}