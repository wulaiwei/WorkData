using System;
using Autofac;
using System.Web.Mvc;
using WorkData.Dto.Entity;
using WorkData.BLL.Interface;

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
            _operationBll.GetList();
            return View();
        }
    }
}