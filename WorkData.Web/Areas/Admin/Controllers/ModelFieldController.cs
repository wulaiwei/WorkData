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
using WorkData.Web.HtmlFactory;

namespace WorkData.Web.Areas.Admin.Controllers
{
    [MvcTokenAutorize]
    public class ModelFieldController : Controller
    {

        private readonly IModelFieldBll _modelFieldBll;
        public ModelFieldController(IModelFieldBll modelFieldBll)
        {
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
            var pageEntity = PageListHepler.BuildPageEntity(pageIndex, 8, "ModelFieldId", "ASC");
            var data = _modelFieldBll.Page(pageEntity);

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

                    var modelFieldDto = _modelFieldBll.HttpGetSave(saveState);

                    ViewBag.SaveState = saveState.ToJson();
                    return View(modelFieldDto);

                case OperationState.Remove:
                    _modelFieldBll.HttpGetSave(saveState);
                    return RedirectToAction("Index", "ModelField");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]

        [ActionDescription(Name = "保存", Action = "Save")]
        public ActionResult Save(ModelFieldDto model)
        {
            var saveState = BusinessHelper.BuildSaveState(Request);

            var htmlMessae= CreateHtmlHelper.ProcessCreateHtmlFactory(model);
            model.HtmlTemplate = htmlMessae.Html;
            model.FieldType = Convert.ToInt16(htmlMessae.FieldType);

            _modelFieldBll.HttpPostSave(model, saveState);

            return RedirectToAction("Index", "ModelField");
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
            var validateEntity = _modelFieldBll.Validate(param);

            return validateEntity.ToJson();
        }
        #endregion
    }
}