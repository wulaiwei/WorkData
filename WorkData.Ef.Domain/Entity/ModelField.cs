using System;
using System.Collections.Generic;

namespace WorkData.EF.Domain.Entity
{
    /// <summary>
    /// 模型字段
    /// </summary>
    public sealed class ModelField
    {
        public ModelField()
        {
            this.Models = new List<Model>();
        }


        /// <summary>
        /// 模型字段ID
        /// </summary>
        public int ModelFieldId { get; set; }

        /// <summary>
        ///字段名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字段代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string ControlType { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 验证提示信息
        /// </summary>
        public string ValidTipMsg { get; set; }

        /// <summary>
        /// 正则表达式
        /// </summary>
        public string ValidPattern { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ValidErrorMsg { get; set; }

        /// <summary>
        /// 是否系统字段
        /// </summary>
        public bool? IsSystemField { get; set; }

        /// <summary>
        /// 选项级
        /// </summary>
        public string ItemOption { get; set; }

        /// <summary>
        /// 模板生成
        /// </summary>
        public string HtmlTemplate { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public int FieldType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        #region 外键
        public ICollection<Model> Models { get; set; }
        #endregion
    }
}
