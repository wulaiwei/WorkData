namespace WorkData.Dto.Entity
{
    /// <summary>
    /// 描述内容
    /// </summary>
    public sealed class ContentDescriptionFieldDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ContentDescriptionFieldId { get; set; }

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
        public string FieldValue { get; set; }

        #region 外键
        public ContentDto Content { get; set; }
        #endregion
    }
}
