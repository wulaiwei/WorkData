// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Web 
// 文件名：SaveState.cs
// 创建标识：吴来伟 2016-10-28
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using WorkData.Util.Enum;

namespace WorkData.Util.Entity
{
    public class SaveState
    {
        /// <summary>
        /// 键值
        /// </summary>
        public object Key { get; set; }
       
        /// <summary>
        /// 操作
        /// </summary>
        public OperationState OperationState { get; set; }
    }
}