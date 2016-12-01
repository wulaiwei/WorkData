using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WorkData.EF.Domain.Entity;

namespace WorkData.EF.Domain.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.UserId);

            this.Property(t => t.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.LoginName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.Salt)
                .IsRequired()
                .HasMaxLength(5);

            this.Property(t => t.Name)
                .HasMaxLength(200);

            this.Property(t => t.CellPhone)
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(200);

            this.Property(t => t.Address)
                .HasMaxLength(500);

            this.Property(t => t.Qq)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("EF_USER");
            this.Property(t => t.UserId).HasColumnName("USER_ID");
            this.Property(t => t.LoginName).HasColumnName("LOGIN_NAME");
            this.Property(t => t.Password).HasColumnName("PASSWORD");
            this.Property(t => t.Salt).HasColumnName("SALT");
            this.Property(t => t.IsLock).HasColumnName("IS_LOCK");
            this.Property(t => t.Name).HasColumnName("NAME");
            this.Property(t => t.CellPhone).HasColumnName("CELL_PHONE");
            this.Property(t => t.Email).HasColumnName("EMAIL");
            this.Property(t => t.Address).HasColumnName("ADDRESS");
            this.Property(t => t.Qq).HasColumnName("Qq");
            this.Property(t => t.WeiChatNumber).HasColumnName("WEICHAT_NUMBER");
            this.Property(t => t.AddTime).HasColumnName("ADD_TIME");

            //this.HasMany(t => t.Resources)
            //    .WithMany(t => t.Users)
            //    .Map(m =>
            //    {
            //        m.ToTable("EF_USER_PRIVILEGE");
            //        m.MapLeftKey("USER_ID");
            //        m.MapRightKey("PRIVILEGE_ID");
            //    });

            this.HasMany(p => p.Roles)
              .WithMany(c => c.Users)
            .Map(manyToMany => manyToMany
              .ToTable("EF_USER_ROLE", "dbo")
              .MapLeftKey("USER_ID")
              .MapRightKey("ROLE_ID"));
        }
    }
}