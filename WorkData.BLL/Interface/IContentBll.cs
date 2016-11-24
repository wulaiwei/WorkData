// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.BLL 
// 文件名：IContentBll.cs
// 创建标识：吴来伟 2016-11-18
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System.Collections.Generic;
using WorkData.Dto.Entity;
using WorkData.Util.Entity;

namespace WorkData.BLL.Interface
{
    public interface IContentBll
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
        /// <param name="listJson"></param>
        /// <returns></returns>
        IEnumerable<ContentDto> Page(PageEntity pageEntity, int categoryId,string listJson);


        /// <summary>
        /// 查询内容
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        ContentDto Query(SaveState saveState);


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        ContentDto HttpGetSave(SaveState saveState);


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="contentDto"></param>
        /// <param name="saveState"></param>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        void HttpPostSave(ContentDto contentDto, SaveState saveState, Dictionary<string, object> dictionary);

    }
}