using System.Data.Entity.Migrations;

namespace WorkData.EF.Domain.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DbEntity>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            ContextKey = "WorkData.EF.Domain.DbEntity";
        }

        protected override void Seed(DbEntity context)
        {
        }
    }
}