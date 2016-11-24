// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Util 
// 文件名：FromDesign.cs
// 创建标识：吴来伟 2016-11-16
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------
namespace WorkData.Util.Entity
{
    /// <summary>
    /// 表单设计
    /// </summary>
    public class FromDesign
    {
        /// <summary>
        /// 标签ID
        /// </summary>
        public int ModelFieldId { get; set; }

        /// <summary>
        /// 标签名
        /// </summary>
        public string ModelFieldName { get; set; }

        /// <summary>
        /// 标签模板
        /// </summary>
        public string ModelFieldTempalte { get; set; }

        /// <summary>
        /// 标签代码
        /// </summary>
        public string ModelFieldCode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Priority { get; set; }
    }
}