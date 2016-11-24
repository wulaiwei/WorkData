// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Service 
// 文件名：IModelFieldService.cs
// 创建标识：吴来伟 2016-11-14
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
    public interface IModelFieldService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        ModelFieldDto Query(object key);

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        IEnumerable<ModelFieldDto> GetList();

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageEntity"></param>
        /// <returns></returns>
        IEnumerable<ModelFieldDto> Page(PageEntity pageEntity);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        void Add(ModelFieldDto entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        void Update(ModelFieldDto entity);

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
        ModelFieldDto Query(string param);
    }
}