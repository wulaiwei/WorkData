using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkData.BLL.Interface;
using WorkData.Dto.Entity;
using WorkData.Token;
using WorkData.Util;

namespace WorkData.Web.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        private readonly IResourceBll _resourceBll;

        public BaseController(IResourceBll resourceBll)
        {
            _resourceBll = resourceBll;
        }

        #region 重写基类在Action执行之前的事情
        /// <summary>
        /// 重写基类在Action执行之前的事情
        /// </summary>
        /// <param name="filterContext">重写方法的参数</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        #endregion 重写基类在Action执行之前的事情

        #region 重写基类在Action执行之后的事情

        /// <summary>
        /// 重写基类在Action执行之后的事情
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
   
            base.OnException(filterContext);
        }

        #endregion 重写基类在Action执行之后的事情
    }
}