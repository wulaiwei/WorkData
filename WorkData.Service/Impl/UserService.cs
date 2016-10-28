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

using EFDTO.Entity;
using System;
using System.Linq;
using System.Linq.Expressions;
using WorkData.Code.AutoMapper;
using WorkData.EF.Domain.Entity;
using WorkData.Infrastructure.IRepositories;
using WorkData.Infrastructure.IUnitOfWorks;
using WorkData.Service.Interface;

namespace WorkData.Service.Impl
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRepository<User> _userRepository;
        private IRepository<Role> _roleRepository;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.Repository<User>();
            _roleRepository = _unitOfWork.Repository<Role>();
        }

        public void Init()
        {
            _userRepository = _unitOfWork.Repository<User>();
            _roleRepository = _unitOfWork.Repository<Role>();
        }

        public UserDto Get(int id)
        {
            Expression<Func<User, bool>> where = w => w.UserId == id;
            var user = _userRepository.Get(where);
            return AutoMapperHelper.Signle<User, UserDto>(user);
        }

        public void Add(UserDto entity)
        {
            using (_unitOfWork)
            {
                var userEntity = AutoMapperHelper.Signle<UserDto, User>(entity);
                _userRepository.Attach(userEntity);
                _userRepository.Add(userEntity);

                _unitOfWork.Commit();
            }
        }

        public void Update(UserDto entity, int[] array)
        {
            var userInfoEntity = AutoMapperHelper.Signle<UserDto, User>(entity);
            using (_unitOfWork)
            {
                Expression<Func<User, bool>> where = w => w.UserId == userInfoEntity.UserId;
                var user = _userRepository.Get(where);
                _userRepository.CurrentValue(user, userInfoEntity);

                var list = user.Roles.Where(role => !array.Contains(role.RoleId)).ToList();
                list.ForEach(c => user.Roles.Remove(c));

                //2.0  求差集
                var roleIdArray = user.Roles.Select(x => x.RoleId).ToArray();
                var expectedList = array.Except(roleIdArray);
                foreach (var role in expectedList.Select(expected => new Role { RoleId = expected }))
                {
                    _roleRepository.Attach(role);
                    //_roleRepository.Attach(role);
                    user.Roles.Add(role);
                }

                _userRepository.Update(user);
                _unitOfWork.Commit();
            }
        }
    }
}