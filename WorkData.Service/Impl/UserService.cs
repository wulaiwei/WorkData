// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。
// 项目名：WorkData.Service
// 文件名：UserService.cs
// 创建标识：  2016-08-30 17:27
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
using WorkData.Infrastructure.IRepositories;
using WorkData.Infrastructure.IUnitOfWorks;
using WorkData.Service.Interface;
using WorkData.Util.Entity;

namespace WorkData.Service.Impl
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        public void Add(UserDto entity)
        {
          
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public UserDto Get(object key)
        {
            using (_unitOfWork)
            {
                var infoKey = Convert.ToInt32(key);
                var repository = _unitOfWork.Repository<User>();

                var user = repository.Get(infoKey);

                return AutoMapperHelper.Signle<User, UserDto>(user);
            }
        }

        /// <summary>
        /// 查询 + 延迟加载
        /// </summary>
        /// <param name="key"></param>
        /// <param name="includeName"></param>
        /// <returns></returns>
        public UserDto Query(object key, string includeName)
        {
            using (_unitOfWork)
            {
                var infoKey = Convert.ToInt32(key);
                var repository = _unitOfWork.Repository<User>();

                Expression<Func<User, bool>> where = w => w.UserId == infoKey;
                var user = repository.Get(where, includeName);

                return AutoMapperHelper.Signle<User, UserDto>(user);
            }
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageEntity"></param>
        /// <returns></returns>
        public IEnumerable<UserDto> Page(PageEntity pageEntity)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<User>();
                Expression<Func<User, bool>> where = w => w.IsLock==false;

                var infoList = repository.Page(pageEntity, where);

                return AutoMapperHelper.Enumerable<User, UserDto>(infoList);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="array"></param>
        public void Add(UserDto entity, int[] array)
        {
            using (_unitOfWork)
            {
                var info = AutoMapperHelper.Signle<UserDto, User>(entity);

                var repository = _unitOfWork.Repository<User>();
                var roleRepository = _unitOfWork.Repository<Role>();

                foreach (var item in array)
                {
                    var role = new Role
                    {
                        RoleId = item
                    };
                    roleRepository.Attach(role);

                    info.Roles.Add(role);
                }

                repository.Add(info);

                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="array"></param>
        public void Update(UserDto entity, int[] array)
        {
            var userInfoEntity = AutoMapperHelper.Signle<UserDto, User>(entity);
            using (_unitOfWork)
            {
                Expression<Func<User, bool>> where = w => w.UserId == userInfoEntity.UserId;
                var repository = _unitOfWork.Repository<User>();
                var roleRepository = _unitOfWork.Repository<Role>();

                var user = repository.Get(where,"Roles");
                repository.CurrentValue(user, userInfoEntity);

                var list = user.Roles.Where(role => !array.Contains(role.RoleId)).ToList();
                list.ForEach(c => user.Roles.Remove(c));
                if (array!=null)
                {
                    //2.0  求差集
                    var roleIdArray = user.Roles.Select(x => x.RoleId).ToArray();
                    var expectedList = array.Except(roleIdArray);

                    foreach (var role in expectedList.Select(expected => 
                    new Role
                    {
                        RoleId = expected
                    }))
                    {
                        roleRepository.Attach(role);
                        user.Roles.Add(role);
                    }

                }


                repository.Update(user);
                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="key"></param>
        public void Remove(object key)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<User>();

                var info = repository.Get(Convert.ToInt32(key));
                info.IsLock = true;
                repository.Update(info);

                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="param"></param>
        /// <param name="includeName"></param>
        /// <returns></returns>
        public UserDto Query(string param,string includeName)
        {
            using (_unitOfWork)
            {
                var repository = _unitOfWork.Repository<User>();

                Expression<Func<User, bool>> where = w => w.LoginName == param;
                var user = string.IsNullOrEmpty(includeName)? 
                    repository.Get(where):
                    repository.Get(where, includeName);

                return AutoMapperHelper.Signle<User, UserDto>(user);
            }
        }
    }
}