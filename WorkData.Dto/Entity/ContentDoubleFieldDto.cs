namespace WorkData.Dto.Entity
{
    /// <summary>
    /// 浮点型内容容
    /// </summary>
    public sealed class ContentDoubleFieldDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ContentDoubleFieldId { get; set; }

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
        public double? FieldValue { get; set; }

        #region 外键
        public ContentDto Content { get; set; }
        #endregion
    }
}
