using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WorkData.Test.Models.Mapping
{
    public class EF_OPERATIONMap : EntityTypeConfiguration<EF_OPERATION>
    {
        public EF_OPERATIONMap()
        {
            // Primary Key
            this.HasKey(t => t.OPERATION_ID);

            // Properties
            this.Property(t => t.NAME)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CODE)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("EF_OPERATION");
            this.Property(t => t.OPERATION_ID).HasColumnName("OPERATION_ID");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.CODE).HasColumnName("CODE");
            this.Property(t => t.STATUS).HasColumnName("STATUS");
        }
    }
}
