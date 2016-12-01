using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WorkData.Test.Models.Mapping
{
    public class EF_ROLEMap : EntityTypeConfiguration<EF_ROLE>
    {
        public EF_ROLEMap()
        {
            // Primary Key
            this.HasKey(t => t.ROLE_ID);

            // Properties
            this.Property(t => t.NAME)
                .HasMaxLength(200);

            this.Property(t => t.CODE)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("EF_ROLE");
            this.Property(t => t.ROLE_ID).HasColumnName("ROLE_ID");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.CODE).HasColumnName("CODE");
            this.Property(t => t.STATUS).HasColumnName("STATUS");

            // Relationships
            this.HasMany(t => t.EF_USER)
                .WithMany(t => t.EF_ROLE)
                .Map(m =>
                    {
                        m.ToTable("EF_USER_ROLE");
                        m.MapLeftKey("ROLE_ID");
                        m.MapRightKey("USER_ID");
                    });


        }
    }
}
