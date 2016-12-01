using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WorkData.Test.Models.Mapping;

namespace WorkData.Test.Models
{
    public partial class WorkDataContext : DbContext
    {
        static WorkDataContext()
        {
            Database.SetInitializer<WorkDataContext>(null);
        }

        public WorkDataContext()
            : base("Name=WorkDataContext")
        {
        }

        public DbSet<EF_CATEGORY> EF_CATEGORY { get; set; }
        public DbSet<EF_CONTENT> EF_CONTENT { get; set; }
        public DbSet<EF_CONTENT_DESCRIPTION_FIELD> EF_CONTENT_DESCRIPTION_FIELD { get; set; }
        public DbSet<EF_CONTENT_DOUBLE_FIELD> EF_CONTENT_DOUBLE_FIELD { get; set; }
        public DbSet<EF_CONTENT_INT_FIELD> EF_CONTENT_INT_FIELD { get; set; }
        public DbSet<EF_CONTENT_STRING_FIELD> EF_CONTENT_STRING_FIELD { get; set; }
        public DbSet<EF_CONTENT_TEXT_FIELD> EF_CONTENT_TEXT_FIELD { get; set; }
        public DbSet<EF_CONTENT_TIME_FIELD> EF_CONTENT_TIME_FIELD { get; set; }
        public DbSet<EF_MODEL> EF_MODEL { get; set; }
        public DbSet<EF_MODEL_FIELD> EF_MODEL_FIELD { get; set; }
        public DbSet<EF_OPERATION> EF_OPERATION { get; set; }
        public DbSet<EF_PRIVILEGE> EF_PRIVILEGE { get; set; }
        public DbSet<EF_RESOURCE> EF_RESOURCE { get; set; }
        public DbSet<EF_ROLE> EF_ROLE { get; set; }
        public DbSet<EF_USER> EF_USER { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EF_CATEGORYMap());
            modelBuilder.Configurations.Add(new EF_CONTENTMap());
            modelBuilder.Configurations.Add(new EF_CONTENT_DESCRIPTION_FIELDMap());
            modelBuilder.Configurations.Add(new EF_CONTENT_DOUBLE_FIELDMap());
            modelBuilder.Configurations.Add(new EF_CONTENT_INT_FIELDMap());
            modelBuilder.Configurations.Add(new EF_CONTENT_STRING_FIELDMap());
            modelBuilder.Configurations.Add(new EF_CONTENT_TEXT_FIELDMap());
            modelBuilder.Configurations.Add(new EF_CONTENT_TIME_FIELDMap());
            modelBuilder.Configurations.Add(new EF_MODELMap());
            modelBuilder.Configurations.Add(new EF_MODEL_FIELDMap());
            modelBuilder.Configurations.Add(new EF_OPERATIONMap());
            modelBuilder.Configurations.Add(new EF_PRIVILEGEMap());
            modelBuilder.Configurations.Add(new EF_RESOURCEMap());
            modelBuilder.Configurations.Add(new EF_ROLEMap());
            modelBuilder.Configurations.Add(new EF_USERMap());
        }
    }
}
