using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Autofac;
using WorkData.BLL.Interface;
using WorkData.Dto.Entity;

namespace WorkData.Web.Areas.Admin.Controllers
{
    public class ResourceController : Controller
    {

        private readonly IResourceBll _resourceBll;
        public ResourceController(IResourceBll resourceBll)
        {
            _resourceBll = resourceBll;
        }

        /// <summary>
        /// 左侧导航栏
        /// </summary>
        /// <returns></returns>
        public HtmlString Resource()
        {
            return _resourceBll.CreateTopResourceHtml();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}