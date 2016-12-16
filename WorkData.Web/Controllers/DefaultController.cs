using System;
using Autofac;
using System.Web.Mvc;
using WorkData.Dto.Entity;
using WorkData.BLL.Interface;
using WorkData.Util;
using WorkData.Util.Entity;
using WorkData.Web.HtmlFactory;
using System.Linq;
using System.Reflection;
using WorkData.Util.Enum;
using WorkData.Web;

namespace WorkData.Web.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IOperationBll _operationBll;
        private readonly IResourceBll _resourceBll;
        public DefaultController(IOperationBll operationBll, IResourceBll resourceBll)
        {
            _operationBll = operationBll;
            _resourceBll = resourceBll;
        }

        //测试
        // GET: Default
        public ActionResult Index()
        {
            //LoggerHelper.BusinessLog.Error("BusinessLog");
            //LoggerHelper.SystemLog.Error("SystemLog");
            //var info = AssemblyHelper.LoadAction("WorkData.Web");
            //var list = _resourceBll.GetList();
            //var infoList = list.Select(x => new AuthConfig
            //{
            //    ControllerName = x.ControllerName,
            //    ResourceId = x.ResourceId,
            //    ResourceUrl = x.ResourceUrl,
            //    Roles = ""
            //}).ToList();
            //AuthConfigXmlHelper.CreateAuthConfigXml(infoList, info, Api.PhysicsUrl + "/Config/AuthConfig.xml");

            //AuthConfigXmlHelper.UpdateConfig(Api.PhysicsUrl + "/Config/AuthConfig.xml", info);


            //var assembly = Assembly.Load("WorkData.Web");    //加载程序集
      
            return View();
        }
    }
}