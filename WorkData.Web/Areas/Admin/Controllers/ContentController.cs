using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
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
    public class ContentController : Controller
    {
        private readonly IContentBll _contentBll;
        private readonly ICategoryBll _categoryBll;
        private readonly IModelBll _modelBll;

        public ContentController(IContentBll contentBll, ICategoryBll categoryBll, IModelBll modelBll)
        {
            _contentBll = contentBll;
            _categoryBll = categoryBll;
            _modelBll = modelBll;
        }


        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionDescription(Name = "列表", Action = "Index")]
        public ActionResult Index(int pageIndex = 1)
        {
            var key = Request.QueryString["CategoryKey"];
            var pageEntity = PageListHepler.BuildPageEntity(pageIndex, 8, "ContentId", "DESC");
            var category = _categoryBll.Query(key, false);
            if (category == null) throw new ArgumentNullException(nameof(category));

            var data = _contentBll.Page(pageEntity, category.CategoryId,category.ListJson);

            var timeFormat = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            ViewBag.Data= JsonConvert.SerializeObject(data, Formatting.Indented, timeFormat);
            ViewBag.ListHead = category.ListHead;
            ViewBag.ListTempalte = category.ListTempalte;
            ViewBag.CategoryId = category.CategoryId;

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

            var key = Request.QueryString["CategoryKey"];
            var category = _categoryBll.Query(key, false);
            ViewBag.CategoryKey = key;
            if (category == null) throw new ArgumentNullException(nameof(category));

            switch (saveState.OperationState)
            {
                case OperationState.Add:
                case OperationState.Update:
                    var contentDto = _contentBll.HttpGetSave(saveState);
    
                    contentDto.ModelId= category.ModelId;
                    contentDto.CategoryId = category.CategoryId;
                    ViewBag.FormTempalte = category.FormTemplate;
                    ViewBag.SaveState = saveState.ToJson();
                    var timeFormat = new IsoDateTimeConverter {DateTimeFormat = "yyyy-MM-dd HH:mm:ss"};
                    ViewBag.Content = JsonConvert.SerializeObject(contentDto,Formatting.Indented, timeFormat);

                    return View();

                case OperationState.Remove:
   
                    _contentBll.HttpGetSave(saveState);

                    return RedirectToAction("Index", "Content", new { CategoryKey = category.CategoryId });
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [ActionDescription(Name = "保存", Action = "Save")]
        public ActionResult Save(ContentDto entity)
        {
            var model = _modelBll.Query(entity.ModelId);
            var saveState = BusinessHelper.BuildSaveState(Request);
            var dictionary = model.ModelFields.
                ToDictionary<ModelFieldDto, string, object>
                (item =>
                item.Code, item => Request.Form[item.Code]);

            _contentBll.HttpPostSave(entity, saveState, dictionary);

            return RedirectToAction("Index", "Content", new { CategoryKey = entity.CategoryId});
        }

    
    }
}