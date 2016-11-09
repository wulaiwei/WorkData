using System.Collections.Generic;

namespace WorkData.Dto.Entity
{
    public sealed class PrivilegeDto
    {
        public PrivilegeDto()
        {
            this.Roles = new List<RoleDto>();
            //this.Users = new List<UserDto>();
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

        public OperationDto Operation { get; set; }
        public ResourceDto Resource { get; set; }
        public ICollection<RoleDto> Roles { get; set; }
        //public ICollection<UserDto> Users { get; set; }

        #endregion 外键
    }
}