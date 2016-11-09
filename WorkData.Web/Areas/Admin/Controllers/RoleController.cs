using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkData.BLL.Interface;
using WorkData.Dto.Entity;
using WorkData.Util;
using WorkData.Util.Enum;

namespace WorkData.Web.Areas.Admin.Controllers
{
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
        public ActionResult Save()
        {
            var saveState = BusinessHelper.BuildSaveState(Request);
            if (saveState == null) throw new ArgumentNullException(nameof(saveState));

            switch (saveState.OperationState)
            {
                case OperationState.Add:
                case OperationState.Update:
                    var roleDto = _roleBll.Query(saveState);

                    var resourceTree = _resourceBll.GetSourceTree(false, "Privileges.Operation");

                    ViewBag.ResourceTree = resourceTree;
                    ViewBag.SaveState = saveState.ToJson();

                    return View(roleDto);
                case OperationState.Remove:
                    //逻辑删除
                    _roleBll.HttpGetSave(saveState);

                    return RedirectToAction("Index");
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
        public ActionResult Save(RoleDto model)
        {
            var saveState = BusinessHelper.BuildSaveState(Request);

            var operationList = Request["privilegeList"];

            var array = BusinessHelper.BreakUpStr(operationList, ',');

            _roleBll.HttpPostSave(model, saveState, array);

            return RedirectToAction("Index");
        }

        #region Ajax操作

        /// <summary>
        /// 验证唯一性
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string Validate()
        {
            var param = Request["param"];
            var validateEntity = _roleBll.Validate(param);

            return validateEntity.ToJson();
        }
        #endregion
    }
}