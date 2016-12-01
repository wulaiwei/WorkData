using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WorkData.Test.Models.Mapping
{
    public class EF_USERMap : EntityTypeConfiguration<EF_USER>
    {
        public EF_USERMap()
        {
            // Primary Key
            this.HasKey(t => t.USER_ID);

            // Properties
            this.Property(t => t.LOGIN_NAME)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PASSWORD)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.NAME)
                .HasMaxLength(200);

            this.Property(t => t.CELL_PHONE)
                .HasMaxLength(50);

            this.Property(t => t.EMAIL)
                .HasMaxLength(200);

            this.Property(t => t.ADDRESS)
                .HasMaxLength(500);

            this.Property(t => t.Qq)
                .HasMaxLength(50);

            this.Property(t => t.SALT)
                .IsRequired()
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("EF_USER");
            this.Property(t => t.USER_ID).HasColumnName("USER_ID");
            this.Property(t => t.LOGIN_NAME).HasColumnName("LOGIN_NAME");
            this.Property(t => t.PASSWORD).HasColumnName("PASSWORD");
            this.Property(t => t.IS_LOCK).HasColumnName("IS_LOCK");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.CELL_PHONE).HasColumnName("CELL_PHONE");
            this.Property(t => t.EMAIL).HasColumnName("EMAIL");
            this.Property(t => t.ADDRESS).HasColumnName("ADDRESS");
            this.Property(t => t.Qq).HasColumnName("Qq");
            this.Property(t => t.WEICHAT_NUMBER).HasColumnName("WEICHAT_NUMBER");
            this.Property(t => t.ADD_TIME).HasColumnName("ADD_TIME");
            this.Property(t => t.SALT).HasColumnName("SALT");
        }
    }
}
