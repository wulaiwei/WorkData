// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。
// 项目名：Auto.Fac.Model
// 文件名：DbEntity.cs
// 创建标识：  2016-08-22 15:24
// 创建描述：
//
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using WorkData.EF.Domain.Entity;
using WorkData.EF.Domain.Mapping;
using WorkData.EF.Domain.Migrations;

namespace WorkData.EF.Domain
{
    /// <summary>
    ///     数据上下文
    /// </summary>
    public class DbEntity : DbContext
    {
        public DbEntity()
                : base("name=Connection")
        {
            this.Configuration.AutoDetectChangesEnabled = true;//对多对多，一对多进行curd操作时需要为true
            this.Configuration.LazyLoadingEnabled = false;
            //this.Configuration.AutoDetectChangesEnabled = false;//禁止状态追踪
            //this.Configuration.ProxyCreationEnabled = false;//禁止动态拦截System.Data.Entity.DynamicProxies.
            //自动创建表，如果Entity有改到就更新到表结构
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DbEntity, Configuration>());
            //Database.SetInitializer<DbEntity>(null);
        }

        #region 关联数据上下文

        /// <summary>
        ///     用户
        /// </summary>
        public DbSet<User> User { get; set; }

        /// <summary>
        ///     角色
        /// </summary>
        public DbSet<Role> Role { get; set; }

        #endregion 关联数据上下文

        /// <summary>
        ///     重写模型创建函数
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //默认移除级联删除
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); //移除复数表名的契约

            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            //防止黑幕交易 要不然每次都要访问 EdmMetadata这个表

            //注册配置文件
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new OperationMap());
            modelBuilder.Configurations.Add(new PrivilegeMap());
            modelBuilder.Configurations.Add(new ResourceMap());
        }
    }
}