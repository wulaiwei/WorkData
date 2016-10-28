// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.BLL 
// 文件名：IResourceBll.cs
// 创建标识：吴来伟 2016-10-28
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkData.Dto.Entity;

namespace WorkData.BLL.Interface
{
    public interface IResourceBll
    {
        /// <summary>
        /// ResourceHtml
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        HtmlString CreateTopResourceHtml(int parentId = 0);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        IList<ResourceDto> GetSourceTree(int parentId = 0);

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