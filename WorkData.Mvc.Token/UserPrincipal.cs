using System.Linq;
using System.Security.Principal;

namespace WorkData.Mvc.Token
{
    /// <summary>
    /// 用户安全主体
    /// </summary>
    public class UserPrincipal : IPrincipal
    {
        public UserPrincipal(IIdentity identity)
        {
            Identity = identity;
        }


        /// <summary>
        /// 用户身份标识
        /// </summary>
        public IIdentity Identity { get; }

        /// <summary>
        /// 是否具有指定角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            var roles = (Identity as UserIdentity).Roles;
            return roles != null && roles.Intersect(new[] {role.Trim()}).Any();
        }
    }
}