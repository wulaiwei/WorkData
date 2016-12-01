// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.BLL 
// 文件名：RoleBll.cs
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
using WorkData.Util.Entity;
using WorkData.Util.Enum;

namespace WorkData.BLL.Impl
{
    public class RoleBll: IRoleBll
    {
        private readonly IRoleService _roleService;
        public RoleBll(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleDto> Page(PageEntity pageEntity)
        {
            return _roleService.Page(pageEntity);
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleDto> GetList()
        {
            return _roleService.GetList();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        public RoleDto Query(SaveState saveState)
        {
            var roleDto = new RoleDto();
            return saveState.OperationState == OperationState.Add ? roleDto : 
                _roleService.Query(saveState.Key, "Resources");
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        public RoleDto HttpGetSave(SaveState saveState)
        {
            var roleDto = new RoleDto();
            switch (saveState.OperationState)
            {
                case OperationState.Add:
                    break;
                case OperationState.Update:
                    roleDto = _roleService.Query(saveState.Key);
                    break;
                case OperationState.Remove:
                    _roleService.Remove(saveState.Key);
                    break;
                default:
                    break;
            }
            return roleDto;
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="roleDto"></param>
        /// <param name="saveState"></param>
        /// <param name="array"></param>
        public void HttpPostSave(RoleDto roleDto, SaveState saveState, int[] array)
        {
            switch (saveState.OperationState)
            {
                case OperationState.Add:
                    _roleService.Add(roleDto, array);
                    break;
                case OperationState.Update:
                    _roleService.Update(roleDto, array);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 验证角色代码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ValidateEntity Validate(string param)
        {
            var validateEntity = new ValidateEntity();
            if (string.IsNullOrEmpty(param))
            {
                validateEntity.Info = "代码不可为空";
                validateEntity.Status = "n";
                return validateEntity;
            }

            var roleDto = _roleService.Query(param);
            if (roleDto == null)
            {
                validateEntity.Info = "该代码可使用！";
                validateEntity.Status = "y";
            }
            else
            {
                validateEntity.Info = "该代码已被占用，请更换！";
                validateEntity.Status = "n";
            }
            return validateEntity;
        }
    }
}