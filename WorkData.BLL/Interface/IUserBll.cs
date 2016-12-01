// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.BLL 
// 文件名：IUserBll.cs
// 创建标识：吴来伟 2016-11-08
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System.Collections.Generic;
using WorkData.Dto.Entity;
using WorkData.Util.Entity;

namespace WorkData.BLL.Interface
{
    public interface IUserBll
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserDto> Page(PageEntity pageEntity);

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        UserDto Query(SaveState saveState);

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        UserDto Query(string loginName);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        UserDto HttpGetSave(SaveState saveState);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="saveState"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        void HttpPostSave(UserDto userDto, SaveState saveState, int[] array);

        /// <summary>
        /// 验证 名称是否唯一
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        ValidateEntity Validate(string param);
    }
}