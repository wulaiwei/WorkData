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
    public class CategoryController : Controller
    {
        private readonly IModelBll _modelBll;
        private readonly IModelFieldBll _modelFieldBll;
        private readonly ICategoryBll _categoryBll;
        public CategoryController(IModelBll modelBll, IModelFieldBll modelFieldBll, ICategoryBll categoryBll)
        {
            _modelBll = modelBll;
            _modelFieldBll = modelFieldBll;
            _categoryBll = categoryBll;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [OperationFilter]
        [ActionDescription(Name = "列表", Action = "Index")]
        [HttpGet]
        public ActionResult Index()
        {
            var infoList = _categoryBll.GetList();

            return View(infoList);
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
                    var categoryDto = _categoryBll.Query(saveState);

                    var categoryTree = _categoryBll.GetList();
                    var modelList = _modelBll.GetList();

                    ViewBag.ModelList = modelList;
                    ViewBag.CategoryTree = categoryTree;
                    ViewBag.SaveState = saveState.ToJson();

                    return View(categoryDto);

                case OperationState.Remove:
                    //逻辑删除
                    _categoryBll.HttpGetSave(saveState);

                    return RedirectToAction("Index", "Category");
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
        public ActionResult Save(CategoryDto model)
        {
            var saveState = BusinessHelper.BuildSaveState(Request);

            var modelId = Request["ModelId"];

            _categoryBll.HttpPostSave(model, saveState, Convert.ToInt32(modelId));

            return RedirectToAction("Index", "Category");
        }

        #region 表单排版设计
        /// <summary>
        /// 表单排版设计
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionDescription(Name = "表单排版", Action = "DesignForm")]
        public ActionResult DesignForm()
        {
            var modelKey = Request.QueryString["ModelKey"];
            var key = Request.QueryString["Key"];
            ViewBag.Key = key;

            var category = _categoryBll.Query(key);
            if (!string.IsNullOrEmpty(category.FormJson))
            {
                var modelInfo = new ModelDto()
                {
                    ModelId = Convert.ToInt32(modelKey),
                    ModelFields = category.FormJson.ToList<ModelFieldDto>()
                };
                return View(modelInfo);
            }

            var model = _modelBll.Query(modelKey);
            return View(model);
        }

        /// <summary>
        /// 表单排版设计
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [ActionDescription(Name = "保存表单排版", Action = "DesignForm")]
        public void DesignForm(string json, string key)
        {
            var info = json.ToList<ModelFieldDto>();

            var formTemplate = CreateHtmlHelper.CreateForm(info);

            _categoryBll.AjaxSave(key, formTemplate, json, null, null, null);
        }
        #endregion

        /// <summary>
        /// 列表页排版设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionDescription(Name = "列表排版", Action = "DesignIndex")]
        public ActionResult DesignIndex()
        {
            var modelKey = Request.QueryString["ModelKey"];
            var key = Request.QueryString["Key"];
            ViewBag.Key = key;
            var category = _categoryBll.Query(key);
            var modelInfo = new ModelDto()
            {
                ModelId = Convert.ToInt32(modelKey)
            };
            if (!string.IsNullOrEmpty(category.ListJson))
            {
                modelInfo.ModelFields = category.ListJson.ToList<ModelFieldDto>();
            }

            ViewBag.Model = modelInfo;

            var model = _modelBll.Query(modelKey);
            return View(model);
        }

        /// <summary>
        /// 表单排版设计
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [ActionDescription(Name = "保存列表排版", Action = "DesignIndex")]
        public void DesignIndex(string json, string key)
        {
            var info = json.ToList<ModelFieldDto>();

            var listHead = CreateHtmlHelper.CreateListHead(info);
            var listTr = CreateHtmlHelper.CreateTrList(info);

            _categoryBll.AjaxSave(key, null, null, listTr,listHead, json);
        }


        #region Ajax
        /// <summary>
        /// 验证唯一性
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionDescription(Name = "验证", Action = "Validate")]
        public string Validate()
        {
            var param = Request["param"];
            var validateEntity = _categoryBll.Validate(param);

            return validateEntity.ToJson();
        }
        #endregion

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
    }
}