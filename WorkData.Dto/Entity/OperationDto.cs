using System.Collections.Generic;

namespace WorkData.Dto.Entity
{
    public sealed class OperationDto
    {
        public OperationDto()
        {
            this.Resources = new List<ResourceDto>();
        }

        /// <summary>
        ///操作ID
        /// </summary>

        public int OperationId { get; set; }

        /// <summary>
        ///操作名称
        /// </summary>

        public string Name { get; set; }

        /// <summary>
        ///操作代码
        /// </summary>

        public string Code { get; set; }

        /// <summary>
        ///状态
        /// </summary>

        public bool Status { get; set; }

        /// <summary>
        /// Html Class
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// Html Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 事件
        /// </summary>
        public string OnClick { get; set; }

        /// <summary>
        /// 样式
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// 操作类别
        /// </summary>
        public int? OperationCategory { get; set; }
        #region 外键

        public ICollection<ResourceDto> Resources { get; set; }

        #endregion 外键
    }
}