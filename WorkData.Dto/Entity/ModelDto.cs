using System.Collections.Generic;
using WorkData.EF.Domain.Entity;

namespace WorkData.Dto.Entity
{
    /// <summary>
    /// 模型
    /// </summary>
    public sealed class ModelDto
    {
        public ModelDto()
        {
            this.Categorys = new List<CategoryDto>();
            this.Contents = new List<ContentDto>();
            this.ModelFields = new List<ModelFieldDto>();
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
        public ICollection<CategoryDto> Categorys { get; set; }
        public ICollection<ContentDto> Contents { get; set; }
        public ICollection<ModelFieldDto> ModelFields { get; set; } 
        #endregion
    }
}
