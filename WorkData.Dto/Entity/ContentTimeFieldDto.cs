using System;

namespace WorkData.Dto.Entity
{
    /// <summary>
    /// 时间内容表
    /// </summary>
    public sealed class ContentTimeFieldDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ContentTimeFieldId { get; set; }

        /// <summary>
        /// 内容ID
        /// </summary>
        public int? ContentId { get; set; }

        /// <summary>
        /// 字段代码
        /// </summary>
        public string FieldCode { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public DateTime? FieldValue { get; set; }

        #region 外键
        public ContentDto Content { get; set; } 
        #endregion
    }
}
