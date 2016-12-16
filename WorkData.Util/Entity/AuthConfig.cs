// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Util 
// 文件名：AuthConfig.cs
// 创建标识：吴来伟 2016-11-25
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------
namespace WorkData.Util.Entity
{
    public class AuthConfig
    {
        /// <summary>
        /// 资源ID
        /// </summary>
        public int ResourceId { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// 资源代码
        /// </summary>
        public string ResourceUrl { get; set; }

        /// <summary>
        /// Action
        /// </summary>
        public string  Action { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string Roles { get; set; }


        /// <summary>
        /// Action描述名
        /// </summary>
        public string Name{ get; set; }
    }
}