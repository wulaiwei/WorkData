using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkData.BLL.Interface;
using WorkData.Dto.Entity;
using WorkData.Mvc.Token;
using WorkData.Util;

namespace WorkData.Web.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private readonly IResourceBll _resourceBll;
        private readonly IUserBll _userBll;
        private readonly IRoleBll _roleBll;
        public LoginController(IResourceBll resourceBll, IUserBll userBll, IRoleBll roleBll)
        {
            _resourceBll = resourceBll;
            _userBll = userBll;
            _roleBll = roleBll;
        }

        /// <summary>
        /// 登陆页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var user = Session["User"] as UserDto;
            if (user != null)
            {
                return RedirectToAction("Index", "Manager");
            }
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection fc)
        {
            if (!string.IsNullOrEmpty(Request["name"]) && !string.IsNullOrEmpty(Request["pwd"]))
            {
                var name = Request["name"];
                var pwd = Request["pwd"];

                var user = _userBll.Query(name);
                if (user == null)
                {
                    SetTempData("用户不存在,请核对账号是否正确！");
                    return RedirectToAction("Index");
                }
                if (user.IsLock)
                {
                    SetTempData("用户已锁定,请联系管理员进行解锁！");
                    return RedirectToAction("Index");
                }
                if (DesEncrypt.Encrypt(pwd, user.Salt) != user.Password)
                {
                    SetTempData("密码错误，请重新输入！");
                    return RedirectToAction("Index");
                }
                var roleIdList = user?.Roles.Select(x => x.RoleId).Distinct();

                //登陆重定向
                Session.Add("User", user);
                Session.Add("RoleIdList", roleIdList);

                var userIdentity = new UserIdentity(user.LoginName, true, 7200);
                foreach (var role in user.Roles)
                {
                    userIdentity.Roles.Add(role.Code);
                }

                //认证
                var token = AuthManager.Login(userIdentity);

                CacheHelper.Insert(user.LoginName, token, 7200);
                return RedirectToAction("Index", "Manager");
            }
            else
            {
                SetTempData("用户名或者密码不能为空！");
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public RedirectToRouteResult LoginOut()
        {
            Session.Remove("User");
            Session.Remove("Roles");

            //CacheHelper.RemoveAllCache();

            return RedirectToAction("Index");
        }


        /// <summary>
        /// 设置信息
        /// </summary>
        /// <param name="message"></param>
        public void SetTempData(string message)
        {
            TempData["ExMessage"] = message;
        }
    }
}