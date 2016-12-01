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
        #region 过于相似 未避免混淆进行拆分  
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        IList<ResourceDto> GetSourceTree(bool isAll, string includeName, int parentId = 0, bool isLock = false);

        /// <summary>
        /// 递归拉取树结构 (只针对后台导航生成模块)
        /// </summary>
        /// <param name="array"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        IList<ResourceDto> GetSourceTree(IEnumerable<int> array, int parentId = 0); 
        #endregion

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<ResourceDto> GetList();


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <param name="includeName"></param>
        /// <returns></returns>
        ResourceDto Query(object key, string includeName);

        /// <summary>
        /// 查询  +  指定列 
        /// </summary>
        /// <param name="sourcePropertyName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        ResourceDto Query(string sourcePropertyName, object param);


        /// <summary>
        /// 查询  +  指定列 (特例)
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="resourceUrl"></param>
        /// <returns></returns>
        ResourceDto Query(string controllerName, string resourceUrl);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        ResourceDto Query(object key);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="array"></param>
        void Add(ResourceDto entity,int[] array);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        void Remove(object key);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        void Update(ResourceDto entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="array"></param>
        void Update(ResourceDto entity, int[] array);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        ResourceDto Query(string param);
    }
}