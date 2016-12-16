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
    public class UserController : Controller
    {
        private readonly IUserBll _userBll;
        private readonly IRoleBll _roleBll;
        public UserController(IUserBll userBll, IRoleBll roleBll)
        {
            _userBll = userBll;
            _roleBll = roleBll;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [OperationFilter]
        [HttpGet]
        [ActionDescription(Name ="列表",Action = "Index")]
        public ActionResult Index(int pageIndex = 1)
        {
            var pageEntity = PageListHepler.BuildPageEntity(pageIndex, 8, "UserId", "ASC");
            var data = _userBll.Page(pageEntity);

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

                    var userDto = _userBll.HttpGetSave(saveState);
                    var roleList = _roleBll.GetList();

                    ViewBag.RoleList = roleList;
                    ViewBag.SaveState = saveState.ToJson();

                    return View(userDto);
                case OperationState.Remove:
                    //逻辑删除
                    _userBll.HttpGetSave(saveState);

                    return RedirectToAction("Index", "User");
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
        public ActionResult Save(UserDto model)
        {
            var saveState = BusinessHelper.BuildSaveState(Request);

            var roleList = Request["roleList"];

            var array = BusinessHelper.BreakUpStr(roleList, ',');

            _userBll.HttpPostSave(model, saveState, array);

            return RedirectToAction("Index", "User");
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