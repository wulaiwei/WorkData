using System;
using Autofac;
using System.Web.Mvc;
using WorkData.Dto.Entity;
using WorkData.BLL.Interface;
using WorkData.Web.HtmlFactory;

namespace WorkData.Web.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IOperationBll _operationBll;
        public DefaultController(IOperationBll operationBll)
        {
            _operationBll = operationBll;
        }

        //测试
        // GET: Default
        public ActionResult Index()
        {
            var model = new ModelFieldDto()
            {
                Name="ces"
            };
            var s= CreateHtmlHelper.CreateTip(model);
            _operationBll.GetList();
            return View();
        }
    }
}