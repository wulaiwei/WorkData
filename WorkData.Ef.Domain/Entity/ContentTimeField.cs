using System;

namespace WorkData.EF.Domain.Entity
{
    /// <summary>
    /// 时间内容表
    /// </summary>
    public sealed class ContentTimeField
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
        public Content Content { get; set; } 
        #endregion
    }
}
