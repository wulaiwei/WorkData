using System.Collections.Generic;
using Newtonsoft.Json;

namespace WorkData.EF.Domain.Entity
{
    public sealed class Resource
    {
        public Resource()
        {
            this.Operations = new List<Operation>();
            this.Roles = new List<Role>();
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
        /// 资源代码
        /// </summary>
        public string Code { get; set; }

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

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { get; set; }


        #region 外键

        [JsonIgnore]
        public ICollection<Operation> Operations { get; set; }

        [JsonIgnore]
        public ICollection<Role> Roles { get; set; }

        #endregion 外键
    }
}