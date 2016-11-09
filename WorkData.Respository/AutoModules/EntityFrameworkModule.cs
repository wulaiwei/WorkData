// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Respository 
// 文件名：EntityFrameworkModule.cs
// 创建标识：吴来伟 2016-11-04
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System.Data.Entity;
using Autofac;
using WorkData.EF.Domain;
using WorkData.Infrastructure.IRepositories;
using WorkData.Infrastructure.ITransactions;
using WorkData.Infrastructure.IUnitOfWorks;
using WorkData.Respository.Repositories;
using WorkData.Respository.Transactions;
using WorkData.Respository.UnitOfWorks;

namespace WorkData.Respository.AutoModules
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