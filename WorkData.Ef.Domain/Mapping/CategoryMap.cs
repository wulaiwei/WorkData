using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WorkData.EF.Domain.Entity;

namespace WorkData.EF.Domain.Mapping
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.CategoryId);

            // Properties
            this.Property(t => t.CategoryId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .HasMaxLength(50);


            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("EF_CATEGORY");
            this.Property(t => t.CategoryId).HasColumnName("CATEGORY_ID");
            this.Property(t => t.ParentId).HasColumnName("PARENT_ID");
            this.Property(t => t.ModelId).HasColumnName("MODEL_ID");
            this.Property(t => t.Name).HasColumnName("NAME");
            this.Property(t => t.Layer).HasColumnName("LAYER");
            this.Property(t => t.Status).HasColumnName("STATUS");
            this.Property(t => t.HasLevel).HasColumnName("HAS_LEVEL");
            this.Property(t => t.Sort).HasColumnName("SORT");
            this.Property(t => t.Code).HasColumnName("CODE");
            this.Property(t => t.FormTemplate).HasColumnName("FORM_TEMPLATE");
            this.Property(t => t.ListTempalte).HasColumnName("LIST_TEMPLATE");
            this.Property(t => t.FormJson).HasColumnName("FORM_JSON");
            this.Property(t => t.ListJson).HasColumnName("LIST_JSON");
            this.Property(t => t.ListHead).HasColumnName("LIST_HEAD");

            // Relationships
            //this.HasMany(t => t.Contents)
            //    .WithMany(t => t.Categorys)
            //    .Map(m =>
            //        {
            //            m.ToTable("EF_CATEGORY_CONTENT");
            //            m.MapLeftKey("CATEGORY_ID");
            //            m.MapRightKey("CONTENT_ID");
            //        });

            this.HasOptional(t => t.Model)
                .WithMany(t => t.Categorys)
                .HasForeignKey(d => d.ModelId);


        }
    }
}
