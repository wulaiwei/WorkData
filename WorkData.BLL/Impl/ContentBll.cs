// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.BLL 
// 文件名：ContentBll.cs
// 创建标识：吴来伟 2016-11-18
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using WorkData.BLL.Interface;
using WorkData.Dto.Entity;
using WorkData.Service.Interface;
using WorkData.Util;
using WorkData.Util.Entity;
using WorkData.Util.Enum;
using System.Linq;

namespace WorkData.BLL.Impl
{
    public class ContentBll:IContentBll
    {
        private readonly IContentService _contentService;
        public ContentBll(IContentService contentService)
        {
            _contentService = contentService;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ContentDto Query(object key)
        {
            return _contentService.Query(key);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageEntity"></param>
        /// <param name="categoryId"></param>
        /// <param name="listJson"></param>
        /// <returns></returns>
        public IEnumerable<ContentDto> Page(PageEntity pageEntity,int categoryId, string listJson)
        {
            var listTemplate = listJson.ToList<dynamic>();
            var arr = listTemplate.Select(x => x.Code).ToArray();

            return _contentService.Page(pageEntity,categoryId, arr);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        public ContentDto Query(SaveState saveState)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        public ContentDto HttpGetSave(SaveState saveState)
        {
            var contentDto = new ContentDto();
            switch (saveState.OperationState)
            {
                case OperationState.Add:
                    break;
                case OperationState.Update:
                    contentDto = _contentService.Query(saveState.Key);
                    break;
                case OperationState.Remove:
                    _contentService.Remove(saveState.Key);
                    break;
                default:
                    break;
            }
            return contentDto;
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="contentDto"></param>
        /// <param name="saveState"></param>
        /// <param name="dictionary"></param>
        public void HttpPostSave(ContentDto contentDto, SaveState saveState, Dictionary<string, object> dictionary)
        {
            switch (saveState.OperationState)
            {
                case OperationState.Add:
                    _contentService.Add(contentDto, dictionary);
                    break;
                case OperationState.Update:
                    _contentService.Update(contentDto, dictionary);
                    break;
                case OperationState.Remove:
                    _contentService.Remove(saveState.Key);
                    break;
                default:
                    break;
            }
        }
    }
}