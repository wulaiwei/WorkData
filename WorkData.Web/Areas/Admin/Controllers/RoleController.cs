using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkData.BLL.Interface;
using WorkData.Dto.Entity;
using WorkData.Mvc.Token;
using WorkData.Util;
using WorkData.Util.Entity;
using WorkData.Util.Enum;
using WorkData.Web.Filter;

namespace WorkData.Web.Areas.Admin.Controllers
{
    [MvcTokenAutorize]
    public class RoleController : Controller
    {
        private readonly IRoleBll _roleBll;
        private readonly IResourceBll _resourceBll;

        public RoleController(IRoleBll roleBll, IResourceBll resourceBll)
        {
            _roleBll = roleBll;
            _resourceBll = resourceBll;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [OperationFilter]
        [ActionDescription(Name = "列表", Action = "Index")]
        public ActionResult Index(int pageIndex = 1)
        {
            var pageEntity = PageListHepler.BuildPageEntity(pageIndex, 8, "RoleId", "ASC");
            var data = _roleBll.Page(pageEntity);

            var page = PageListHepler.BuildPagedList(data, pageEntity);
            return View(page);
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionDescription(Name = "编辑", Action = "Save")]
        public ActionResult Save()
        {
            var saveState = BusinessHelper.BuildSaveState(Request);
            if (saveState == null) throw new ArgumentNullException(nameof(saveState));

            ViewBag.ActionList = AssemblyHelper.LoadAction(WebConfig.AssemblyName);

            switch (saveState.OperationState)
            {
                case OperationState.Add:
                case OperationState.Update:
                    var roleDto = _roleBll.Query(saveState);

                    var resourceTree = _resourceBll.GetSourceTree(false, null);

                    ViewBag.ResourceTree = resourceTree;
                    ViewBag.SaveState = saveState.ToJson();

                    ViewBag.InfoList= string.Join(",", roleDto.Resources.Select(p => p.ResourceId));
                    ViewBag.AuthConfigList = AuthConfigXmlHelper.GetAuthConfigListByXml(Api.PhysicsUrl + "/Config/AuthConfig.xml",
                        roleDto.Code);
                    return View(roleDto);
                case OperationState.Remove:
                    //逻辑删除
                    _roleBll.HttpGetSave(saveState);

                    return RedirectToAction("Index", "Role");
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
        [ValidateAntiForgeryToken]
        [ActionDescription(Name = "保存", Action = "Save")]
        public ActionResult Save(RoleDto model)
        {
            var saveState = BusinessHelper.BuildSaveState(Request);

            var resourceList = Request["resourceList"];

            var array = BusinessHelper.BreakUpStr(resourceList, ',');
            var arrayStr = BusinessHelper.BreakUpOptions(resourceList, ',');
            _roleBll.HttpPostSave(model, saveState, array);

            var actionList = Request["actionList"];

            var actionArr = BusinessHelper.BreakUpOptions(actionList, '|');
            //资源授权
            AuthConfigXmlHelper.UpateRolesAuthConfigByXml(Api.PhysicsUrl + "/Config/AuthConfig.xml"
                , arrayStr, model.Code);
            //Action授权
            AuthConfigXmlHelper.UpateActionRolesAuthConfigByXml(Api.PhysicsUrl + "/Config/AuthConfig.xml"
                , model.Code,actionArr);

            return RedirectToAction("Index", "Role");
        }

        #region Ajax操作

        /// <summary>
        /// 验证唯一性
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionDescription(Name = "验证", Action = "Validate")]
        public string Validate()
        {
            var param = Request["param"];
            var validateEntity = _roleBll.Validate(param);

            return validateEntity.ToJson();
        }

        #endregion
    }
}