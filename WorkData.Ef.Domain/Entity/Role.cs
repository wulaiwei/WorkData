using System.Collections.Generic;

namespace WorkData.EF.Domain.Entity
{
    public sealed class Role
    {
        public Role()
        {
            //this.Resources = new List<Privilege>();
            this.Users = new List<User>();
            this.Resources = new List<Resource>();
        }

        /// <summary>
        ///角色ID
        /// </summary>

        public int RoleId { get; set; }

        /// <summary>
        ///角色名称
        /// </summary>

        public string Name { get; set; }

        /// <summary>
        ///角色代码
        /// </summary>

        public string Code { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Status { get; set; }

        #region 外键

        //public ICollection<Privilege> Resources { get; set; }
        public ICollection<User> Users { get; set; }

        public ICollection<Resource> Resources { get; set; }

        #endregion 外键
    }
}