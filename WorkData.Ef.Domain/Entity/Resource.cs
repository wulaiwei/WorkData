using Newtonsoft.Json;
using System.Collections.Generic;
using WorkData.EF.Domain.Entity;

namespace EFModel.Entity
{
    public sealed class Resource
    {
        public Resource()
        {
            this.Privileges = new List<Privilege>();
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

        /// <summary>
        /// 操作列表
        /// </summary>
        public List<Operation> OperationList { get; set; }

        #region 外键

        [JsonIgnore]
        public ICollection<Privilege> Privileges { get; set; }

        #endregion 外键
    }
}