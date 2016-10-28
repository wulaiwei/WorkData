using Autofac;

namespace WorkData.Code.Ioc
{
    /// <summary>
    ///依赖注入容器
    /// </summary>
    public static class Ioc
    {
        static Ioc()
        {
            Boot.Start();
        }

        /// <summary>
        /// Autofac IOC容器
        /// </summary>
        public static IContainer Container { get; set; }
    }
}