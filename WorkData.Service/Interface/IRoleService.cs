// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。
// 项目名：WorkData.Service
// 文件名：IRoleService.cs
// 创建标识：  2016-08-31 16:20
// 创建描述：
//
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using WorkData.Dto.Entity;

namespace WorkData.Service.Interface
{
    public interface IRoleService
    {
        RoleDto Get(int id);
    }
}