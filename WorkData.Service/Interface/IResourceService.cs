// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Service 
// 文件名：IResourceService.cs
// 创建标识：吴来伟 2016-10-27
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using WorkData.Dto.Entity;

namespace WorkData.Service.Interface
{
    public interface IResourceService
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        IList<ResourceDto> GetSourceTree(int parentId=0);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        IQueryable<ResourceDto> GetList();

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        IQueryable<ResourceDto> GetList(int[] array);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        void Add(ResourceDto entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        void Remove(ResourceDto entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        void Update(ResourceDto entity);
    }
}