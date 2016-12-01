using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WorkData.Util.Entity;

namespace WorkData.Token
{
    /// <summary>
    /// 基于令牌的授权验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,AllowMultiple =true,Inherited =true)]
    public class TokenAutorizeAttribute : AuthorizationFilterAttribute
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


        public override void OnAuthorization(HttpActionContext context)
        {


            if (!AuthManager.TryAuthorize(context,_roles))
            {
                var respMessage = ResponseProvider.Error("你没有被授权访问此资源。", 401);
                context.Response = context.Request.CreateResponse(HttpStatusCode.OK, respMessage);
            }
        }
    }
}