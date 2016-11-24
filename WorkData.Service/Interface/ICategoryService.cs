// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Service 
// 文件名：ICategoryService.cs
// 创建标识：吴来伟 2016-11-15
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
    public interface ICategoryService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        CategoryDto Query(object key);

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        IEnumerable<CategoryDto> GetList();

        /// <summary>
        /// 查询 + 延迟加载
        /// </summary>
        /// <param name="key"></param>
        /// <param name="includeName"></param>
        /// <returns></returns>
        CategoryDto Query(object key, string includeName);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="modelId"></param>
        void Add(CategoryDto entity, int modelId);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="modelId"></param>
        void Update(CategoryDto entity, int modelId);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        void Remove(object key);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        CategoryDto Query(string param);

    }
}