using System.Collections.Generic;

namespace WorkData.Dto.Entity
{
    public sealed class RoleDto
    {
        public RoleDto()
        {
            this.Resources = new List<ResourceDto>();
            this.Users = new List<UserDto>();
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

        public ICollection<ResourceDto> Resources { get; set; }
        public ICollection<UserDto> Users { get; set; }

        #endregion 外键
    }
}