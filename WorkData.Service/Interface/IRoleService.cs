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

using System.Collections.Generic;
using WorkData.Dto.Entity;
using WorkData.Util.Entity;

namespace WorkData.Service.Interface
{
    public interface IRoleService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        RoleDto Query(object key);

        /// <summary>
        /// 查询+延迟加载
        /// </summary>
        /// <param name="key"></param>
        /// <param name="includeName"></param>
        /// <returns></returns>
        RoleDto Query(object key, string includeName);


        /// <summary>
        /// 查询可用角色
        /// </summary>
        /// <returns></returns>
        IEnumerable<RoleDto> GetList();

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageEntity"></param>
        /// <returns></returns>
        IEnumerable<RoleDto> Page(PageEntity pageEntity);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="array"></param>
        void Add(RoleDto entity, int[] array);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        void Remove(object key);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="array"></param>
        void Update(RoleDto entity, int[] array);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        RoleDto Query(string param);
    }
}