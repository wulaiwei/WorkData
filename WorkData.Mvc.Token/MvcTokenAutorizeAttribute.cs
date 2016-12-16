using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WorkData.Dto.Entity;
using WorkData.Util;
using WorkData.Util.Entity;

namespace WorkData.Mvc.Token
{
    /// <summary>
    /// 基于令牌的授权验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,AllowMultiple =true,Inherited =true)]
    public class MvcTokenAutorizeAttribute : AuthorizeAttribute
    {
        private string[] _roles;

        /// <summary>
        /// 角色
        /// </summary>
        public string Roles
        {
            get { return string.Join(",", _roles); }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _roles = null;
                    return;
                }

                var roleValueSplitArray = value.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

                _roles = roleValueSplitArray.Select(role => role.Trim()).ToArray();
            }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //no1: 判断是否登录
            var user = filterContext.HttpContext.Session?["User"] as UserDto;
            var controller= filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var action = filterContext.ActionDescriptor.ActionName;
            var method = filterContext.HttpContext.Request.HttpMethod;
            var url = filterContext.HttpContext.Request.RawUrl;
            var categoryKey = filterContext.HttpContext.Request.QueryString["CategoryKey"];
            if (user != null)
            {
                var token = CacheHelper.GetCache(user.LoginName);
                var info = AuthConfigXmlHelper.GetAuthConfigByXml(Api.PhysicsUrl + "/Config/AuthConfig.xml"
                    , url, controller, action, method, categoryKey);
                _roles = BusinessHelper.BreakUpOptions(info.Roles, ',');

                if (!AuthManager.TryAuthorize(filterContext,token.ToString(), _roles))
                {
                    var respMessage = ResponseProvider.Error("你没有被授权访问此资源。", 401);
                    //异常处理模块接入
                }
            }
         

            base.OnAuthorization(filterContext);
        }
    }
}