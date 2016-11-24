// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Service 
// 文件名：IContentService.cs
// 创建标识：吴来伟 2016-11-17
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
    public interface IContentService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        ContentDto Query(object key);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="pageEntity"></param>
        /// <param name="categoryId"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        IEnumerable<ContentDto> Page(PageEntity pageEntity,int categoryId, dynamic[] arr);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dictionary"></param>
        void Add(ContentDto entity, Dictionary<string, object> dictionary);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dictionary"></param>
        void Update(ContentDto entity, Dictionary<string, object> dictionary);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        void Remove(object key);
    }
}