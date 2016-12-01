using System.Collections.Generic;
using System.Security.Principal;

namespace WorkData.Mvc.Token
{
    /// <summary>
    /// 用户身份标识
    /// </summary>
    public class UserIdentity : IIdentity
    {
        /// <summary>
        /// 用户身份标识构造
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="isAuth">是否已认证</param>
        public UserIdentity(string name, bool isAuth)
        {
            IsAuthenticated = isAuth;
            Name = name;
        }

        /// <summary>
        /// 用户身份标识构造
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="isAuth">是否已认证</param>
        /// <param name="expired">有效期</param>
        public UserIdentity(string name, bool isAuth, int expired)
        {
            IsAuthenticated = isAuth;
            Name = name;
            Expired = expired;
        }

        /// <summary>
        /// 用户角色
        /// </summary>
        public List<string> Roles { get; set; } = new List<string>();

        /// <summary>
        /// 用户当前令牌
        /// </summary>
        internal string Token { get; private set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        internal int Expired { get; }

        public string Name { get; }

        public string AuthenticationType { get; } = "auth_token";

        public bool IsAuthenticated { get; }


        internal void SetToken(string token)
        {
            Token = token;
        }
    }
}