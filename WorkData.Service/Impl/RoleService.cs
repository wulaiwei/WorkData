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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WorkData.Code.AutoMapper;
using WorkData.Dto.Entity;
using WorkData.EF.Domain.Entity;
using WorkData.Infrastructure.IUnitOfWorks;
using WorkData.Service.Interface;
using WorkData.Util.Entity;

namespace WorkData.Service.Impl
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public RoleDto Query(object key)
        {
            using (_unitOfWork)
            {
                var infoKey = Convert.ToInt32(key);
                var repository = _unitOfWork.Repository<Role>();

                var role = repository.Get(infoKey);
                return AutoMapperHelper.Signle<Role, RoleDto>(role);
            }

        }

        /// <summary>
        /// 查询+延迟加载
        /// </summary>
        /// <param name="key"></param>
        /// <param name="includeName"></param>
        /// <returns></returns>
        public RoleDto Query(object key, string includeName)
        {
            using (_unitOfWork)
            {
                var infoKey = Convert.ToInt32(key);
                var repository = _unitOfWork.Repository<Role>();

                Expression<Func<Role, bool>> where = w => w.RoleId == infoKey;
                var role = repository.Get(where, includeName);

                return AutoMapperHelper.Signle<Role, RoleDto>(role);
            }
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleDto> GetList()
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Role>();

                Expression<Func<Role, bool>> where = w => w.Status;
                var role = repository.Query(where);

                return AutoMapperHelper.Enumerable<Role, RoleDto>(role);
            }
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageEntity"></param>
        /// <returns></returns>
        public IEnumerable<RoleDto> Page(PageEntity pageEntity)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Role>();
                Expression<Func<Role, bool>> where = w => w.Status;

                var infoList = repository.Page(pageEntity, where);

                return AutoMapperHelper.Enumerable<Role, RoleDto>(infoList);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="array"></param>
        public void Add(RoleDto entity, int[] array)
        {
            using (_unitOfWork)
            {
                var info = AutoMapperHelper.Signle<RoleDto, Role>(entity);

                var repository = _unitOfWork.Repository<Role>();
                var privilegeRepository = _unitOfWork.Repository<Privilege>();

                foreach (var item in array)
                {
                    var privilege = new Privilege
                    {
                       PrivilegeId=item
                    };
                    privilegeRepository.Attach(privilege);

                    info.Privileges.Add(privilege);
                }

                repository.Add(info);

                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        public void Remove(object key)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Role>();

                var info = repository.Get(Convert.ToInt32(key));
                info.Status = false;
                repository.Update(info);

                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="array"></param>
        public void Update(RoleDto entity, int[] array)
        {
            var roleInfoEntity = AutoMapperHelper.Signle<RoleDto, Role>(entity);
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Role>();
                var privilegeRepository = _unitOfWork.Repository<Privilege>();

                Expression<Func<Role, bool>> where = w => w.RoleId == roleInfoEntity.RoleId;

                var role = repository.Get(where, "Privileges");
                repository.CurrentValue(role, roleInfoEntity);

                var list = role.Privileges.Where(p => !array.Contains(p.PrivilegeId)).ToList();
                list.ForEach(c => role.Privileges.Remove(c));

                ////2.0  求差集
                var privilegeArray = role.Privileges.Select(x => x.PrivilegeId).ToArray();
                var expectedList = array.Except(privilegeArray);

                foreach (var privilege in expectedList.Select(expected => new Privilege { PrivilegeId = expected }))
                {
                    privilegeRepository.Attach(privilege);
                    role.Privileges.Add(privilege);
                }

                repository.Update(role);
                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RoleDto Query(string param)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<Role>();

                Expression<Func<Role, bool>> where = w => w.Code == param;
                var role = repository.Get(where);

                return AutoMapperHelper.Signle<Role, RoleDto>(role);
            }
        }
    }
}