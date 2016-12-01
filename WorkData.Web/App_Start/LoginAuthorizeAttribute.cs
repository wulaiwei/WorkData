// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Web 
// 文件名：LoginAuthorizeAttribute.cs
// 创建标识：吴来伟 2016-11-28
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System.Web;
using System.Web.Mvc;

namespace WorkData.Web
{
    public class LoginAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var pass = httpContext.Session?["User"] != null;
            return pass;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Admin/Login/Index");
        }
    }
}