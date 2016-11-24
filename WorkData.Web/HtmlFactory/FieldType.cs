// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Web 
// 文件名：FieldType.cs
// 创建标识：吴来伟 2016-11-10
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System.ComponentModel;

namespace WorkData.Web.HtmlFactory
{
    /// <summary>
    /// 字段类别
    /// </summary>
    public enum FieldType
    {
        [Description("整数")]
        IntField = 0,

        [Description("浮点数")]
        DoubleField = 1,

        [Description("短长度字符串")]
        StringdField = 2,

        [Description("中长度字符串")]
        DescriptionField = 3,

        [Description("文章")]
        TextField = 4,

        [Description("时间")]
        TimeField = 5,
    }
}