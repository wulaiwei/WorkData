// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。
// 项目名：WorkData.Service
// 文件名：RoleService.cs
// 创建标识：  2016-08-31 16:21
// 创建描述：
//
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using WorkData.Code.AutoMapper;
using WorkData.Dto.Entity;
using WorkData.EF.Domain.Entity;
using WorkData.Infrastructure.IRepositories;
using WorkData.Infrastructure.IUnitOfWorks;
using WorkData.Service.Interface;

namespace WorkData.Service.Impl
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _roleRepository;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _roleRepository = unitOfWork.Repository<Role>();
        }

        public RoleDto Get(int id)
        {
            var role = _roleRepository.Get(id);
            return AutoMapperHelper.Signle<Role, RoleDto>(role);
        }
    }
}