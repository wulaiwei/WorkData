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
    public class ModelController : Controller
    {
        private readonly IModelBll _modelBll;
        private readonly IModelFieldBll _modelFieldBll;

        public ModelController(IModelBll modelBll, IModelFieldBll modelFieldBll)
        {
            _modelBll = modelBll;
            _modelFieldBll = modelFieldBll;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        [OperationFilter]
        [ActionDescription(Name = "列表", Action = "Index")]
        public ActionResult Index(int pageIndex = 1)
        {
            var pageEntity = PageListHepler.BuildPageEntity(pageIndex, 8, "ModelId", "ASC");
            var data = _modelBll.Page(pageEntity);

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
            switch (saveState.OperationState)
            {
                case OperationState.Add:
                case OperationState.Update:

                    var modelDto = _modelBll.HttpGetSave(saveState);

                    var modelFieldList = _modelFieldBll.GetList();
                    ViewBag.ModelFieldList = modelFieldList;

                    ViewBag.SaveState = saveState.ToJson();
                    return View(modelDto);

                case OperationState.Remove:
                    _modelBll.HttpGetSave(saveState);
                    return RedirectToAction("Index", "Model");
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
        public ActionResult Save(ModelDto model)
        {
            var saveState = BusinessHelper.BuildSaveState(Request);

            var modelFieldList = Request["ModelFieldList"];

            var array = BusinessHelper.BreakUpStr(modelFieldList, ',');

            _modelBll.HttpPostSave(model, saveState, array);

            return RedirectToAction("Index", "Model");
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
            var validateEntity = _modelBll.Validate(param);

            return validateEntity.ToJson();
        }
        #endregion
    }
}