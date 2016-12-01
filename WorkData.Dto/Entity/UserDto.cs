using System;
using System.Collections.Generic;

namespace WorkData.Dto.Entity
{
    public sealed partial class UserDto
    {
        public UserDto()
        {
            //this.Resources = new List<PrivilegeDto>();
            this.Roles = new List<RoleDto>();
        }

        /// <summary>
        ///用户ID
        /// </summary>

        public int UserId { get; set; }

        /// <summary>
        ///登录名
        /// </summary>

        public string LoginName { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLock { get; set; }

        /// <summary>
        /// 盐值
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        ///密码
        /// </summary>

        public string Password { get; set; }

        /// <summary>
        ///真实姓名
        /// </summary>

        public string Name { get; set; }

        /// <summary>
        ///手机
        /// </summary>

        public string CellPhone { get; set; }

        /// <summary>
        ///邮箱
        /// </summary>

        public string Email { get; set; }

        /// <summary>
        ///地址
        /// </summary>

        public string Address { get; set; }

        /// <summary>
        ///Qq
        /// </summary>

        public string Qq { get; set; }

        /// <summary>
        ///微信
        /// </summary>

        public string WeiChatNumber { get; set; }

        /// <summary>
        ///新增时间
        /// </summary>

        public DateTime? AddTime { get; set; }

        #region 外键
        //public ICollection<PrivilegeDto> Resources { get; set; }
        public ICollection<RoleDto> Roles { get; set; }

        #endregion 外键
    }
}