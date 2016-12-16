// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Web 
// 文件名：ActionDescriptionAttribute.cs
// 创建标识：吴来伟 2016-12-05
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System;

namespace WorkData.Util.Entity
{
    public class ActionDescriptionAttribute: Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Action
        /// </summary>
        public string  Action { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string  Method { get; set; }
    }
}