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
using WorkData.Util.Entity;

namespace WorkData.BLL.Interface
{
    public interface IResourceBll
    {
        /// <summary>
        /// ResourceHtml
        /// </summary>
        /// <param name="array"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        HtmlString CreateTopResourceHtml(IEnumerable<int> array,int parentId = 0);

        /// <summary>
        /// 获取资源树+延迟加载
        /// </summary>
        /// <param name="isAll"></param>
        /// <param name="includeName"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        IList<ResourceDto> GetSourceTree(bool isAll, string includeName, int parentId = 0);


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sourcePropertyName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        ResourceDto Query(string sourcePropertyName,object param);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="resourceUrl"></param>
        /// <returns></returns>
        ResourceDto Query(string controllerName,string resourceUrl);

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        ResourceDto Query(SaveState saveState);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<ResourceDto> GetList();

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        ResourceDto HttpGetSave(SaveState saveState);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="resourceDto"></param>
        /// <param name="saveState"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        void HttpPostSave(ResourceDto resourceDto, SaveState saveState,int[] array);

        /// <summary>
        /// Ajax更新
        /// </summary>
        /// <param name="resourceDto"></param>
        void AjaxUpdate(ResourceDto resourceDto);


        /// <summary>
        /// 验证 代码是否唯一
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        ValidateEntity Validate(string param);

    }
}