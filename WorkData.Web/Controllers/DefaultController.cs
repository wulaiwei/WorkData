using System;
using Autofac;
using System.Web.Mvc;
using WorkData.Dto.Entity;
using WorkData.BLL.Interface;
using WorkData.Util;
using WorkData.Util.Entity;
using WorkData.Web.HtmlFactory;
using System.Linq;
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
            //var model = new ModelFieldDto()
            //{
            //    Name="ces"
            //};
            //var s= CreateHtmlHelper.CreateTip(model);
            //_operationBll.GetList();
            var list= _resourceBll.GetList();
            var infoList = list.Select(x => new AuthConfig
            {
                ControllerName = x.ControllerName,
                ResourceId = x.ResourceId,
                ResourceUrl = x.ResourceUrl,
                Roles = ""
            }).ToList();
            //AuthConfigXmlHelper.CreateAuthConfigXml(infoList,Api.PhysicsUrl + "/Config/AuthConfig.xml");

            //var info= AuthConfigXmlHelper.GetAuthConfigByXml(Api.PhysicsUrl + "/Config/AuthConfig.xml"
            //    , "/Admin/Content/Index?Key=32", "Content");

            //var att = new string[] { "1", "2" };
            //var info = new AuthConfig
            //{
            //    ControllerName = "xx",
            //    ResourceId = 1,
            //    ResourceUrl = "yy",
            //    Roles = "xxx"
            //};

            //AuthConfigXmlHelper.AttachAuthConfigByXml(Api.PhysicsUrl + "/Config/AuthConfig.xml"
            //    , info);

            return View();
        }
    }
}