using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Configuration;
using WorkData.EF.Domain;
using WorkData.EF.Domain.Entity;
using WorkData.Infrastructure.IRepositories;
using WorkData.Infrastructure.ITransactions;
using WorkData.Infrastructure.IUnitOfWorks;
using WorkData.Respository.Repositories;
using WorkData.Respository.Transactions;
using WorkData.Respository.UnitOfWorks;
using System.Data.Entity;
using WorkData.Code.Ioc;
using WorkData.Dto;
using WorkData.Service.Interface;
using WorkData.Dto.Entity;
using HibernatingRhinos.Profiler.Appender.EntityFramework;

namespace WorkData.Test
{
    class Program
    {
        private static Autofac.IContainer Container { get; set; }
        static void Main(string[] args)
        {
            EntityFrameworkProfiler.Initialize();
            AutoMapperConfiguration.Configure();
            InjectByConfig();

            var userService = Container.Resolve<IUserService>();
            //var roleService = Container.Resolve<IRoleService>();
            var userDto = new UserDto() { Id=8,Name="lisi123123 "};
            //var userDto = userService.Get(7);
            //var role = roleService.Get(19);
            //var roleDto = new RoleDto() { Id=23};
            //userDto.Roles.Add(roleDto);
            //userService.Add(userDto);
             var array = new int[] { 23 };
            userService.Update(userDto, array);
            //var roleRepository = manager.Repository<Role>();
            //var userRepository = manager.Repository<User>();
            //var role = new Role()
            //{
            //    Id = 19,
            //    Name = "admin1",
            //    Code = "admin1"
            //};
            //var user = new User()
            //{
            //    Name = "zhangsan5",
            //};
            //user.Roles.Add(role);
            //userRepository.Add(user);

            //manager.Commit();
            Console.ReadKey();


        }

        /// <summary>
        /// 采用配置文件注入
        /// </summary>
        public static void InjectByConfig()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ConfigurationSettingsReader("autofac"));
            Container = builder.Build();

        }
    }
}
