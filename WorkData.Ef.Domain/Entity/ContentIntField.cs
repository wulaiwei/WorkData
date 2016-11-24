using System;

namespace WorkData.EF.Domain.Entity
{
    /// <summary>
    /// 整型内容
    /// </summary>
    public sealed class ContentIntField
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ContentIntFieldId { get; set; }

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
        public int? FieldValue { get; set; }

        #region 外键
        public Content Content { get; set; }
        #endregion
    }
}
