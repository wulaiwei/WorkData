// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Web 
// 文件名：PprocessEntity.cs
// 创建标识：吴来伟 2016-10-28
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

namespace WorkData.Util.Entity
{
    public class ProcessEntity<T>
    {
        public SaveState SaveState { get; set; }
        public T Entity { get; set; }
    }
}