using System;
using System.Collections.Generic;

namespace WorkData.EF.Domain.Entity
{
    /// <summary>
    /// 模型
    /// </summary>
    public sealed class Model
    {
        public Model()
        {
            this.Categorys = new List<Category>();
            this.Contents = new List<Content>();
            this.ModelFields = new List<ModelField>();
        }

        /// <summary>
        /// 模型ID
        /// </summary>
        public int ModelId { get; set; }

        /// <summary>
        /// 模型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 模型代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        #region 外键
        public ICollection<Category> Categorys { get; set; }
        public ICollection<Content> Contents { get; set; }
        public ICollection<ModelField> ModelFields { get; set; } 
        #endregion
    }
}
