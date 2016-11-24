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
using System.Text;
using System.Web;
using WorkData.BLL.Interface;
using WorkData.Dto.Entity;
using WorkData.Service.Interface;
using WorkData.Util.Entity;
using WorkData.Util.Enum;

namespace WorkData.BLL.Impl
{
    public class ResourceBll : IResourceBll
    {
        private readonly IResourceService _resourceService;
        public ResourceBll(IResourceService resourceService, ICategoryService categoryService)
        {
            _resourceService = resourceService;
        }

        /// <summary>
        /// 生成Html结构的Resource树
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        HtmlString IResourceBll.CreateTopResourceHtml(int parentId)
        {
            var sb = new StringBuilder();

            var infoList = _resourceService.GetSourceTree(false, null,parentId);
            var topList = infoList.Where(x => x.Layer == 0);
            foreach (var item in topList)
            {
                sb.AppendLine("<div class=\"list-group\">");
                sb.AppendLine("<h1 title='" + item.ResourceName + "'>");
                if (!string.IsNullOrEmpty(item.ResourceImg))
                {
                    sb.AppendLine("<img src='" + item.ResourceImg + "'/>");
                }
                sb.AppendLine("</h1>");
                sb.AppendLine("<div class=\"list-wrap\">");
                sb.AppendLine("<h2>" + item.ResourceName + "<i></i></h2>");
                if (item.HasLevel)
                {
                    sb.AppendLine(CreateChildResourceHtml(infoList, item.ResourceId));
                }
                sb.AppendLine("</div></div>");
            }
            return new HtmlString(sb.ToString());
        }


        /// <summary>
        /// 获取资源树
        /// </summary>
        /// <param name="isAll"></param>
        /// <param name="includeName"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<ResourceDto> GetSourceTree(bool isAll,string includeName, int parentId = 0)
        {
            return _resourceService.GetSourceTree(isAll, includeName, parentId);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sourcePropertyName"></param>
        /// <param name="method"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public ResourceDto Query(string sourcePropertyName, string method, object param)
        {
            return _resourceService.Query(sourcePropertyName,method, param);
        }

        /// <summary>
        ///  获取列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ResourceDto> GetList()
        {
            return _resourceService.GetList();
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        public ResourceDto Query(SaveState saveState)
        {
            var resourceDto = new ResourceDto();
            return saveState.OperationState == OperationState.Add ?
                resourceDto :
                _resourceService.Query(saveState.Key, "Privileges");
        }

        /// <summary>
        /// Get请求处理
        /// </summary>
        /// <param name="saveState"></param>
        /// <returns></returns>
        public ResourceDto HttpGetSave(SaveState saveState)
        {
            var resourceDto = new ResourceDto();
            switch (saveState.OperationState)
            {
                case OperationState.Add:
                    break;
                case OperationState.Update:
                    resourceDto = _resourceService.Query(saveState.Key);
                    break;
                case OperationState.Remove:
                    _resourceService.Remove(saveState.Key);
                    break;
                default:
                    break;
            }
            return resourceDto;
        }

        /// <summary>
        /// Post请求处理
        /// </summary>
        /// <param name="resourceDto"></param>
        /// <param name="saveState"></param>
        /// <param name="array"></param>
        public void HttpPostSave(ResourceDto resourceDto, SaveState saveState, int[] array)
        {
            var parentResource = _resourceService.Query((object)resourceDto.ParentId);

            switch (saveState.OperationState)
            {
                case OperationState.Add:
                    if (parentResource != null) resourceDto.Layer = parentResource.Layer + 1;
                    _resourceService.Add(resourceDto, array);
                    break;
                case OperationState.Update:
                    _resourceService.Update(resourceDto, array);
                    break;
                default:
                    break;
            }

            if (parentResource == null) return;
            parentResource.HasLevel = true;
            _resourceService.Update(parentResource);
        }

        /// <summary>
        /// Ajax保存
        /// </summary>
        /// <param name="resourceDto"></param>
        public void AjaxUpdate(ResourceDto resourceDto)
        {
            _resourceService.Update(resourceDto,null);
        }

        /// <summary>
        /// 验证代码唯一性
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ValidateEntity Validate(string param)
        {
            var validateEntity = new ValidateEntity();
            if (string.IsNullOrEmpty(param))
            {
                validateEntity.Info = "资源代码不可为空";
                validateEntity.Status = "n";
                return validateEntity;
            }

            var mresourceDto = _resourceService.Query(param);
            if (mresourceDto == null)
            {
                validateEntity.Info = "该资源代码可使用！";
                validateEntity.Status = "y";
            }
            else
            {
                validateEntity.Info = "该资源代码已被占用，请更换！";
                validateEntity.Status = "n";
            }
            return validateEntity;
        }

        #region 私有方法
        /// <summary>
        /// 获取子级菜单
        /// </summary>
        /// <param name="infoList"></param>
        /// <param name="currentId"></param>
        /// <returns></returns>
        private static string CreateChildResourceHtml(IList<ResourceDto> infoList, int currentId)
        {
            var sb = new StringBuilder();
            var childList = infoList.Where(x => x.ParentId == currentId);
            var resourceDtos = childList as ResourceDto[] ?? childList.ToArray();
            if (!resourceDtos.Any()) return sb.ToString();

            sb.AppendLine("<ul style='display: block;'>");
            foreach (var item in resourceDtos)
            {
                sb.AppendLine("<li>");
                sb.AppendLine("<a navid='channel_main'");
                if (!string.IsNullOrEmpty(item.ResourceUrl))
                {
                    sb.AppendLine(" href='" + item.ResourceUrl + "'");
                }
                sb.AppendLine(" target='mainframe'>");
                sb.AppendLine("<span>" + item.ResourceName + "</span></a>");
                //自身迭代
                if (item.HasLevel)
                {
                    var list = CreateChildResourceHtml(infoList, item.ResourceId);
                    sb.AppendLine(list);
                }
                sb.AppendLine("</li>");
            }
            sb.AppendLine("</ul>");
            return sb.ToString();
        } 
        #endregion
    }
}