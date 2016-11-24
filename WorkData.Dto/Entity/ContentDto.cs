using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WorkData.EF.Domain.Entity;

namespace WorkData.Dto.Entity
{
    /// <summary>
    /// 内容表
    /// </summary>
    public class ContentDto
    {
        public ContentDto()
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
        /// 栏目ID
        /// </summary>
        public int? CategoryId { get; set; }


        #region 外键
        [JsonIgnore]
        public ICollection<ContentIntField> ContentIntFields { get; set; }

        [JsonIgnore]
        public ICollection<ContentTimeField> ContentTimeFields { get; set; }

        [JsonIgnore]
        public ICollection<ContentDoubleField> ContentDoubleFields { get; set; }

        [JsonIgnore]
        public ICollection<ContentStringField> ContentStringFields { get; set; }

        [JsonIgnore]
        public ICollection<ContentTextField> ContentTextFields { get; set; }

        [JsonIgnore]
        public ICollection<ContentDescriptionField> ContentDescriptionFields { get; set; }

        [JsonIgnore]
        public Model Model { get; set; }

        [JsonIgnore]
        public CategoryDto Category { get; set; }

        #endregion

        /// <summary>
        /// 动态内容
        /// </summary>
        public ContentValue ContentValue { get; set; }
    }
}
