// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.BLL 
// 文件名：UserBll.cs
// 创建标识：吴来伟 2016-11-08
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System.Collections.Generic;
using WorkData.BLL.Interface;
using WorkData.Dto.Entity;
using WorkData.Service.Interface;
using WorkData.Util;
using WorkData.Util.Entity;
using WorkData.Util.Enum;

namespace WorkData.BLL.Impl
{
    public class UserBll: IUserBll
    {
        private readonly IUserService _userService;
        public UserBll(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageEntity"></param>
        /// <returns></returns>
        public IEnumerable<UserDto> Page(PageEntity pageEntity)
        {
            return _userService.Page(pageEntity);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        public UserDto Query(SaveState saveState)
        {
            var userDto = new UserDto();
            return saveState.OperationState == OperationState.Add ? userDto :
                _userService.Query(saveState.Key, "Roles");
        }

        /// <summary>
        ///登陆
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public UserDto Query(string loginName)
        {
            return _userService.Query(loginName, "Roles");
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        public UserDto HttpGetSave(SaveState saveState)
        {
            var userDto = new UserDto();
            switch (saveState.OperationState)
            {
                case OperationState.Add:
                    break;
                case OperationState.Update:
                    userDto = _userService.Query(saveState.Key, "Roles");
                    userDto.Password = DesEncrypt.Decrypt(userDto.Password, userDto.Salt);
                    break;
                case OperationState.Remove:
                    _userService.Remove(saveState.Key);
                    break;
                default:
                    break;
            }

            return userDto;
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="saveState"></param>
        /// <param name="array"></param>
        public void HttpPostSave(UserDto userDto, SaveState saveState, int[] array)
        {
            switch (saveState.OperationState)
            {
                case OperationState.Add:
                    var salt= Encrypt.GetCheckCode(5);
                    userDto.Salt = salt;
                    userDto.Password= DesEncrypt.Encrypt(userDto.Password, salt); //盐值加密
                    _userService.Add(userDto, array);
                    break;
                case OperationState.Update:
                    userDto.Password = DesEncrypt.Encrypt(userDto.Password, userDto.Salt); //盐值加密
                    _userService.Update(userDto, array);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 验证登录名唯一性
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ValidateEntity Validate(string param)
        {
            var validateEntity = new ValidateEntity();
            if (string.IsNullOrEmpty(param))
            {
                validateEntity.Info = "登录名不可为空";
                validateEntity.Status = "n";
                return validateEntity;
            }

            var userDto = _userService.Query(param,null);
            if (userDto == null)
            {
                validateEntity.Info = "该登录名可使用！";
                validateEntity.Status = "y";
            }
            else
            {
                validateEntity.Info = "该登录名已被占用，请更换！";
                validateEntity.Status = "n";
            }
            return validateEntity;
        }
    }
}