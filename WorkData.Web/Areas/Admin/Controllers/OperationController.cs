﻿using System;
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
    public class OperationController : Controller
    {
        private readonly IOperationBll _operationBll;
        public OperationController(IOperationBll operationBll)
        {
            _operationBll = operationBll;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int pageIndex=1)
        {
            var pageEntity = PageListHepler.BuildPageEntity(pageIndex, 8, "OperationId", "ASC");
            var data = _operationBll.Page(pageEntity);

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

                    var operationDto = _operationBll.HttpGetSave(saveState);

                    ViewBag.SaveState = saveState.ToJson();
                    return View(operationDto);
                case OperationState.Remove:
                    _operationBll.HttpGetSave(saveState);
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
        public ActionResult Save(OperationDto model)
        {
            var saveState = BusinessHelper.BuildSaveState(Request);

            _operationBll.HttpPostSave(model,saveState);

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
            var validateEntity = _operationBll.Validate(param);

            return validateEntity.ToJson();// Json(validateEntity,JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}