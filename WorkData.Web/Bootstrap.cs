#region 导入名称空间

using Autofac;
using Autofac.Configuration;
using System;
using System.Reflection;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using WorkData.Web.Filter;

#endregion 导入名称空间

namespace WorkData.Web
{
    /// <summary>
    /// 引导程序
    /// </summary>
    public static class Bootstrap
    {
        private static bool _isInit;

        static Bootstrap()
        {
            Initiate();
        }

        /// <summary>
        /// Autofac IOC容器
        /// </summary>
        public static IContainer ApplicationContainer { get; set; }

        /// <summary>
        /// 初始化集成框架
        /// </summary>
        [STAThread]
        public static void Initiate()
        {
            if (_isInit) return;

            var builder = new ContainerBuilder();


            builder.RegisterModule(new ConfigurationSettingsReader("autofac"));

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces();


            //注册过滤器 
            builder.RegisterFilterProvider();
            builder.RegisterType<OperationFilter>().PropertiesAutowired();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);


            ApplicationContainer = builder.Build();

            _isInit = true;
        }
    }
}