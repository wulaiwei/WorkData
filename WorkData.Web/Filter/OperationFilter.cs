// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Web 
// 文件名：LoggerFilter.cs
// 创建标识：吴来伟 2016-11-24
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System.Web.Mvc;
using WorkData.BLL.Interface;
using System.Linq;
using WorkData.Web.HtmlFactory;

namespace WorkData.Web.Filter
{
    public class OperationFilter : FilterAttribute, IActionFilter
    {

        public IResourceBll ResourceBll { get; set; }

        public string  TopHtml { get; set; }

        /// <summary>
        /// Action之前
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            var url = filterContext.HttpContext.Request.RawUrl;
            var resource = ResourceBll.Query(controllerName, url);
            if (resource == null) return;

            var topOperations = resource.Operations.Where(x => x.OperationCategory == 0).ToList();
            var listOperations = resource.Operations.Where(x => x.OperationCategory == 1).ToList();
            var topHtml = CreateHtmlHelper.CreateOperationTopList(topOperations);
            var listHtml = CreateHtmlHelper.CreateOperationIndexList(listOperations);

            //filterContext.Controller.ViewData["ResourceKey"] = filterContext.HttpContext.Request.QueryString["ResourceKey"];
            filterContext.Controller.ViewData["TopHtml"] =topHtml;
            filterContext.Controller.ViewData["ListHtml"] = listHtml;

        }

        /// <summary>
        /// Action之后
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
         
        }

        protected void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //在View显示之前
           
        }
    }
}