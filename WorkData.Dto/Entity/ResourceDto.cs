using System.Collections.Generic;

namespace WorkData.Dto.Entity
{
    public sealed class ResourceDto
    {
        public ResourceDto()
        {
            this.Privileges = new List<PrivilegeDto>();
        }

        /// <summary>
        ///资源ID
        /// </summary>

        public int ResourceId { get; set; }

        /// <summary>
        ///父级ID
        /// </summary>

        public int? ParentId { get; set; }

        /// <summary>
        ///资源名称
        /// </summary>

        public string ResourceName { get; set; }

        /// <summary>
        ///资源链接
        /// </summary>
        public string ResourceUrl { get; set; }

        /// <summary>
        ///级别
        /// </summary>

        public int Layer { get; set; }

        /// <summary>
        ///是否锁定
        /// </summary>

        public bool IsLock { get; set; }

        /// <summary>
        ///资源图片
        /// </summary>

        public string ResourceImg { get; set; }

        /// <summary>
        ///排序
        /// </summary>

        public int Sort { get; set; }

        /// <summary>
        ///是否还有子级
        /// </summary>

        public bool HasLevel { get; set; }

        #region 外键

        public ICollection<PrivilegeDto> Privileges { get; set; }

        #endregion 外键
    }
}