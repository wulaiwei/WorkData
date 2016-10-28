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

namespace WorkData.BLL.Impl
{
    public class ResourceBll : IResourceBll
    {
        private readonly IResourceService _resourceService;
        public ResourceBll(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        HtmlString IResourceBll.CreateTopResourceHtml(int parentId)
        {
            var sb = new StringBuilder();

            var infoList = _resourceService.GetSourceTree(parentId);
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

        public IList<ResourceDto> GetSourceTree(int parentId = 0)
        {
            throw new System.NotImplementedException();
        }



        public IQueryable<ResourceDto> GetList()
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<ResourceDto> GetList(int[] array)
        {
            throw new System.NotImplementedException();
        }

        public void Add(ResourceDto entity)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(ResourceDto entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(ResourceDto entity)
        {
            throw new System.NotImplementedException();
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