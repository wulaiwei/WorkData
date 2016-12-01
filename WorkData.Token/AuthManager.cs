using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace WorkData.Token
{
    /// <summary>
    /// 认证/授权管理
    /// </summary>
    public static class AuthManager
    {
        /// <summary>
        /// 访问令牌
        /// </summary>
        private const string AUTH_TOKEN = "auth_token";


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="identity">用户标识</param>
        /// <returns>返回令牌</returns>
        public static string Login(UserIdentity identity)
        {
            var token = GenerateToken();
            identity.SetToken(token);
            if (identity.IsAuthenticated)
            {
                SetPrincipal(identity);
            }

            return token;
        }


        /// <summary>
        /// 注销
        /// </summary>
        public static void Logout()
        {
            if (HttpContext.Current.User == null || !HttpContext.Current.User.Identity.IsAuthenticated)
            {
                throw new InvalidOperationException("Logout 操作必须授权");
            }

            var token = (HttpContext.Current.User.Identity as UserIdentity).Token;
            HttpCacheManager.RemoveCache(token);
            HttpContext.Current.User = null;
        }

        /// <summary>
        /// 尝试授权
        /// </summary>
        /// <param name="context">正在执行的操作上下文</param>
        /// <param name="roles">要验证授权的角色</param>
        /// <returns></returns>
        internal static bool TryAuthorize(HttpActionContext context, params string[] roles)
        {
            // 匿名访问验证
            var allowAnonymous = context.ActionDescriptor
                .GetCustomAttributes<AllowAnonymousAttribute>()
                .Any();

            var token = GetToken(context);
            if (token == null) return allowAnonymous;
            if (roles == null) return true;
            if (!IsValidToken(token)) return false;

            var identity = GetIdentity(token);
            if (identity.Roles != null && identity.Roles.Intersect(roles).Any())
            {
                SetPrincipal(identity); //认证/授权完成后需要设置安全主体到 HTTP上下文
                return true;
            }

            return false;
        }


        /// <summary>
        /// 设置安全主体
        /// </summary>
        /// <param name="identity"></param>
        internal static void SetPrincipal(UserIdentity identity)
        {
            var principal = new UserPrincipal(identity);
            HttpContext.Current.User = principal;
            if (identity.Expired == 0)
            {
                HttpCacheManager.SetCache(identity.Token, identity);
            }
            else
            {
                HttpCacheManager.SetCache(identity.Token, identity, identity.Expired);
            }
        }
        
        /// <summary>
        /// 生成一个令牌
        /// </summary>
        /// <returns></returns>
        internal static string GenerateToken()
        {
            var guidUp = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            var guidLow = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            var tokenBuilder = new StringBuilder();
            tokenBuilder.Append(guidUp.Where(char.IsLetterOrDigit).ToArray());
            tokenBuilder.Append(guidLow.Where(char.IsLetterOrDigit).ToArray());

            return tokenBuilder.ToString();
        }

        /// <summary>
        /// 从请求中获取用户令牌
        /// </summary>
        /// <returns></returns>
        internal static string GetToken(HttpActionContext context)
        {
            var queryString = context.Request.RequestUri.ParseQueryString();
            if (!queryString.AllKeys.Contains(AUTH_TOKEN))
            {
                return null;
            }

            var token = queryString[AUTH_TOKEN];

            return token;
        }

        /// <summary>
        /// 取用户令牌对应的用户标识
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        internal static UserIdentity GetIdentity(string token)
        {
            var identity = HttpCacheManager.GetCache<UserIdentity>(token);

            return identity;
        }


        /// <summary>
        /// 是有效的令牌
        /// </summary>
        /// <returns></returns>
        internal static bool IsValidToken(string token)
        {
            try
            {
                if (null == token)
                {
                    return false;
                }

                var identity = HttpCacheManager.GetCache<UserIdentity>(token);
                return null != identity;
            }
            catch
            {
                return false;
            }
        }
    }
}