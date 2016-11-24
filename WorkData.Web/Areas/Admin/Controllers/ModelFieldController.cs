using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkData.BLL.Interface;
using WorkData.Dto.Entity;
using WorkData.Util;
using WorkData.Util.Enum;
using WorkData.Web.HtmlFactory;

namespace WorkData.Web.Areas.Admin.Controllers
{
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
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Save(ModelFieldDto model)
        {
            var saveState = BusinessHelper.BuildSaveState(Request);

            var htmlMessae= CreateHtmlHelper.ProcessCreateHtmlFactory(model);
            model.HtmlTemplate = htmlMessae.Html;
            model.FieldType = Convert.ToInt16(htmlMessae.FieldType);

            _modelFieldBll.HttpPostSave(model, saveState);

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
            var validateEntity = _modelFieldBll.Validate(param);

            return validateEntity.ToJson();
        }
        #endregion
    }
}