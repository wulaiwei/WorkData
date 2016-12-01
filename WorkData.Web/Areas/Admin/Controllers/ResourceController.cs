using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using WorkData.BLL.Interface;
using WorkData.Dto.Entity;
using WorkData.Mvc.Token;
using WorkData.Util;
using WorkData.Util.Entity;
using WorkData.Util.Enum;

namespace WorkData.Web.Areas.Admin.Controllers
{
    public class ResourceController : Controller
    {

        private readonly IResourceBll _resourceBll;
        private readonly IOperationBll _operationBll;
        public ResourceController(IResourceBll resourceBll, IOperationBll operationBll)
        {
            _resourceBll = resourceBll;
            _operationBll = operationBll;
        }

        /// <summary>
        /// 列表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [MvcTokenAutorize]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Save()
        {
            var saveState = BusinessHelper.BuildSaveState(Request);
            if (saveState == null) throw new ArgumentNullException(nameof(saveState));
            switch (saveState.OperationState)
            {
                case OperationState.Add:
                case OperationState.Update:
                    var resourceDto = _resourceBll.Query(saveState);

                    var resourceTree = _resourceBll.GetSourceTree(false,null);
                    var operationList = _operationBll.GetList();

                    ViewBag.ResourceTree = resourceTree;
                    ViewBag.OperationList = operationList;
                    ViewBag.SaveState = saveState.ToJson();

                    return View(resourceDto);
                case OperationState.Remove:
                    //逻辑删除
                    _resourceBll.HttpGetSave(saveState);
                    return RedirectToAction("Index", "Resource");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(ResourceDto model)
        {
            var saveState = BusinessHelper.BuildSaveState(Request);

            var operationList = Request["OperationList"];

            var array = BusinessHelper.BreakUpStr(operationList,',');

            _resourceBll.HttpPostSave(model, saveState, array);

            var info = new AuthConfig
            {
                ControllerName = model.ControllerName,
                ResourceId = model.ResourceId,
                ResourceUrl = model.ResourceUrl,
                Roles = ""
            };

            if (saveState.OperationState ==OperationState.Add)
            {
                AuthConfigXmlHelper.AttachAuthConfigByXml(Api.PhysicsUrl + "/Config/AuthConfig.xml"
                    , info);
            }
            else
            {
                AuthConfigXmlHelper.UpateResourceAuthConfigByXml(Api.PhysicsUrl + "/Config/AuthConfig.xml"
                   , info);
            }

            return RedirectToAction("Index", "Resource");
        }
        #region Ajax操作


        /// <summary>
        /// 生成指定长度的字符串,即生成strLong个str字符串
        /// </summary>
        /// <param name="strLong">生成的长度</param>
        /// <param name="str">以str生成字符串</param>
        /// <returns></returns>
        public static string StringOfChar(int strLong, string str)
        {
            var returnStr = "";
            for (var i = 0; i < strLong; i++)
            {
                returnStr += str;
            }

            return returnStr;
        }

        /// <summary>
        /// 更新列表
        /// </summary>
        /// <param name="resourceDto"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AjaxUpdate(ResourceDto resourceDto,string action)
        {
            if (action=="Remove")
            {
                resourceDto.IsLock = true;
            }
            _resourceBll.AjaxUpdate(resourceDto);

            var treeList = _resourceBll.GetSourceTree(true,null);
            return Json(treeList, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 验证唯一性
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string Validate()
        {
            var param = Request["param"];
            var validateEntity = _resourceBll.Validate(param);

            return validateEntity.ToJson();
        }

        /// <summary>
        /// 获取资源树列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetResourceTree()
        {
            var treeList = _resourceBll.GetSourceTree(true,null);
            return Json(treeList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 左侧导航栏
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HtmlString Resource()
        {
            var user = Session["User"] as UserDto;
            if (user != null)
            {
                var cacheResource = CacheHelper.GetCache(user.LoginName+ "Resource");
                if (cacheResource != null) return cacheResource as HtmlString;
            }

            var array = Session["RoleIdList"] as IEnumerable<int>;
            var html = _resourceBll.CreateTopResourceHtml(array);

            if (user != null) CacheHelper.Insert(user.LoginName + "Resource", html, 10);

            return html;
        }
        #endregion


    }
}