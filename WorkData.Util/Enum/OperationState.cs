// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Web 
// 文件名：OperationState.cs
// 创建标识：吴来伟 2016-10-28
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System.ComponentModel;

namespace WorkData.Util.Enum
{
    public enum OperationState
    {
        [Description("添加")]
        Add = 0,

        [Description("更新")]
        Update = 1,

        [Description("删除")]
        Remove = 2
    }
}