// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Web 
// 文件名：SystemConfigurationSectionHandler.cs
// 创建标识：吴来伟 2016-11-03
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System.Configuration;
using System.Xml;

namespace WorkData.Web
{
    public class SystemConfigurationSectionHandler: IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            throw new System.NotImplementedException();
        }
    }
}