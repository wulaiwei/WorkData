using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WorkData.EF.Domain.Entity;

namespace WorkData.EF.Domain.Mapping
{
    public class ModelFieldMap : EntityTypeConfiguration<ModelField>
    {
        public ModelFieldMap()
        {
            // Primary Key
            this.HasKey(t => t.ModelFieldId);

            // Properties
            this.Property(t => t.ModelFieldId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ControlType)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DefaultValue)
                .HasMaxLength(50);

            this.Property(t => t.ValidTipMsg)
                .HasMaxLength(200);

            this.Property(t => t.ValidPattern)
                .HasMaxLength(500);

            this.Property(t => t.ValidErrorMsg)
                .HasMaxLength(200);

            this.Property(t => t.ItemOption)
                .HasMaxLength(500);

            this.Property(t => t.HtmlTemplate)
               .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("EF_MODEL_FIELD");
            this.Property(t => t.ModelFieldId).HasColumnName("MODEL_FIELD_ID");
            this.Property(t => t.Name).HasColumnName("NAME");
            this.Property(t => t.Code).HasColumnName("CODE");
            this.Property(t => t.ControlType).HasColumnName("CONTROL_TYPE");
            this.Property(t => t.DefaultValue).HasColumnName("DEFAULT_VALUE");
            this.Property(t => t.ValidTipMsg).HasColumnName("VALID_TIP_MSG");
            this.Property(t => t.ValidPattern).HasColumnName("VALID_PATTERN");
            this.Property(t => t.ValidErrorMsg).HasColumnName("VALID_ERROR_MSG");
            this.Property(t => t.IsSystemField).HasColumnName("IS_SYSTEM_FIELD");
            this.Property(t => t.ItemOption).HasColumnName("ITEM_OPTION");
            this.Property(t => t.HtmlTemplate).HasColumnName("HTML_TEMPLATE");
            this.Property(t => t.Status).HasColumnName("STATUS");
            this.Property(t => t.FieldType).HasColumnName("FIELD_TYPE");

        }
    }
}
