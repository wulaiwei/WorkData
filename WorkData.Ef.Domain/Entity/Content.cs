using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace WorkData.EF.Domain.Entity
{
    /// <summary>
    /// 内容表
    /// </summary>
    public sealed class Content
    {
        public Content()
        {
            this.ContentDescriptionFields = new List<ContentDescriptionField>();
            this.ContentDoubleFields = new List<ContentDoubleField>();
            this.ContentIntFields = new List<ContentIntField>();
            this.ContentStringFields = new List<ContentStringField>();
            this.ContentTextFields = new List<ContentTextField>();
            this.ContentTimeFields = new List<ContentTimeField>();
        }

        /// <summary>
        /// 内容ID
        /// </summary>
        public int ContentId { get; set; }
        /// <summary>
        /// 模型ID
        /// </summary>
        public int? ModelId { get; set; }

        /// <summary>
        /// 模型ID
        /// </summary>
        public int? CategoryId { get; set; }

        #region 外键
        public ICollection<ContentIntField> ContentIntFields { get; set; }
        public ICollection<ContentTimeField> ContentTimeFields { get; set; }
        public ICollection<ContentDoubleField> ContentDoubleFields { get; set; }
        public ICollection<ContentStringField> ContentStringFields { get; set; }
        public ICollection<ContentTextField> ContentTextFields { get; set; }
        public ICollection<ContentDescriptionField> ContentDescriptionFields { get; set; }
        public Model Model { get; set; }
        public Category Category { get; set; }
        #endregion

    }
}
