using System.Collections.Generic;

namespace WorkData.Dto.Entity
{
    /// <summary>
    /// 栏目表
    /// </summary>
    public sealed class CategoryDto
    {
        public CategoryDto()
        {
            this.Contents = new List<ContentDto>();
        }

        /// <summary>
        /// 栏目ID
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        ///父级ID
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 模型ID
        /// </summary>
        public int? ModelId { get; set; }

        /// <summary>
        /// 栏目名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///级别
        /// </summary>
        public int Layer { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        ///排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        ///是否还有子级
        /// </summary>
        public bool HasLevel { get; set; }

        /// <summary>
        /// 表单模板
        /// </summary>
        public string FormTemplate { get; set; }

        /// <summary>
        /// 表单模板JSON
        /// </summary>
        public string FormJson { get; set; }

        /// <summary>
        /// 列表模板JSON
        /// </summary>
        public string ListJson { get; set; }

        /// <summary>
        /// 列表头部
        /// </summary>
        public string ListHead { get; set; }

        /// <summary>
        /// 列表模板
        /// </summary>
        public string ListTempalte { get; set; }
        #region 外键
        public ModelDto Model { get; set; }
        public ICollection<ContentDto> Contents { get; set; } 
        #endregion
    }
}
