using System.Collections.Generic;

namespace WorkData.EF.Domain.Entity
{
    public sealed class Privilege
    {
        public Privilege()
        {
            Roles = new List<Role>();
            //Users = new List<User>();
        }

        /// <summary>
        ///权限ID
        /// </summary>

        public int PrivilegeId { get; set; }

        /// <summary>
        ///资源ID
        /// </summary>

        public int ResourceId { get; set; }

        /// <summary>
        ///操作ID
        /// </summary>

        public int OperationId { get; set; }

        #region 外键

        public Operation Operation { get; set; }
        public Resource Resource { get; set; }
        public ICollection<Role> Roles { get; set; }
        //public ICollection<User> Users { get; set; }

        #endregion 外键
    }
}