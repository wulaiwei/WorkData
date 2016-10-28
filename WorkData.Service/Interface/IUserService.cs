// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。
// 项目名：WorkData.Service
// 文件名：IUserService.cs
// 创建标识：  2016-08-30 17:26
// 创建描述：
//
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using EFDTO.Entity;

namespace WorkData.Service.Interface
{
    public interface IUserService
    {
        UserDto Get(int id);

        void Add(UserDto entity);

        void Update(UserDto entity, int[] array);
    }
}