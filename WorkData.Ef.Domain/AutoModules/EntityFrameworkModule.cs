using Autofac;
using System.Data.Entity;
using WorkData.Infrastructure.IRepositories;
using WorkData.Infrastructure.ITransactions;
using WorkData.Infrastructure.IUnitOfWorks;
using WorkData.Respository.Repositories;
using WorkData.Respository.Transactions;
using WorkData.Respository.UnitOfWorks;

namespace WorkData.EF.Domain.AutoModules
{
    public class EntityFrameworkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfTransaction>().As<ITransaction>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<DbEntity>().As<DbContext>();

            builder.RegisterGeneric(typeof(BaseRepository<>))
                .As(typeof(IRepository<>));
        }
    }
}