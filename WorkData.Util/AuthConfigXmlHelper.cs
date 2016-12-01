// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Util 
// 文件名：XmlHelper.cs
// 创建标识：吴来伟 2016-11-30
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using WorkData.Util.Entity;

namespace WorkData.Util
{
    public class AuthConfigXmlHelper
    {
        /// <summary>
        /// 创建权限文件
        /// </summary>
        /// <param name="list"></param>
        /// <param name="fileName"></param>
        public static void CreateAuthConfigXml(List<AuthConfig> list,string fileName)
        {
            var xElement = new XElement
             (
                 "AuthConfigs",
                 list.Select(p => new XElement
                     (
                     "AuthConfig",
                     new XAttribute("ResourceId", p.ResourceId),
                     new XAttribute("ControllerName", string.IsNullOrEmpty(p.ControllerName) ? "" : p.ControllerName),
                     new XAttribute("ResourceUrl", string.IsNullOrEmpty(p.ResourceUrl) ? "" : p.ResourceUrl),
                     new XAttribute("Roles", string.IsNullOrEmpty(p.Roles) ? "" : p.Roles)
                     ))
                );
            xElement.Save(fileName);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="authConfig"></param>
        public static void UpateResourceAuthConfigByXml(string fileName, AuthConfig authConfig)
        {
            var xElement = XElement.Load(fileName);
            var infoList = from e in xElement.Elements("AuthConfig")
                           let xAttribute = e.Attribute("ResourceId")
                           where xAttribute != null && xAttribute.Value== authConfig.ResourceId.ToString()
                           select e;
            var info = infoList.FirstOrDefault();
            info?.SetAttributeValue("ControllerName", authConfig.ControllerName);
            info?.SetAttributeValue("ResourceUrl", authConfig.ResourceUrl);
            xElement.Save(fileName);
        }


        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="array"></param>
        /// <param name="roleCode"></param>
        public static void UpateRolesAuthConfigByXml(string fileName, string[] array,string roleCode)
        {
            var xElement = XElement.Load(fileName);
            var infoList = from e in xElement.Elements("AuthConfig")
                let xAttribute = e.Attribute("ResourceId")
                where xAttribute != null && array.Contains(xAttribute.Value)
                           select e;

            foreach (var info in infoList)
            {

                var roles = info.Attribute("Roles")?.Value;

                if (roles == null || roles.Contains(roleCode)) continue;

                roles = roles.Length>0? roles+ "," + roleCode: roleCode;
                info.SetAttributeValue("Roles", roles);
            }

            var otherInfoList = from e in xElement.Elements("AuthConfig")
                           let xAttribute = e.Attribute("ResourceId")
                           where xAttribute != null && !array.Contains(xAttribute.Value)
                           select e;

            foreach (var info in otherInfoList)
            {

                var roles = info.Attribute("Roles")?.Value;

                if (roles == null || !roles.Contains(roleCode)) continue;
                var arr= BusinessHelper.BreakUpOptions(roles, ',');

                roles = roles.Replace(roleCode + ",","").Replace(roleCode, "");
                info.SetAttributeValue("Roles", roles);
            }

            xElement.Save(fileName);
        }

        /// <summary>
        /// 追加数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="authConfig"></param>
        public static void AttachAuthConfigByXml(string fileName, AuthConfig authConfig)
        {
            var xElement = XElement.Load(fileName);
            var info = new XElement
                     (
                     "AuthConfig",
                     new XAttribute("ResourceId", authConfig.ResourceId),
                     new XAttribute("ControllerName", string.IsNullOrEmpty(authConfig.ControllerName) ? "" : authConfig.ControllerName),
                     new XAttribute("ResourceUrl", string.IsNullOrEmpty(authConfig.ResourceUrl) ? "" : authConfig.ResourceUrl),
                     new XAttribute("Roles", string.IsNullOrEmpty(authConfig.Roles) ? "" : authConfig.Roles)
                     );

            xElement.Add(info);
            xElement.Save(fileName);
        }

        /// <summary>
        /// 查询指定数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="resourceUrl"></param>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public static AuthConfig GetAuthConfigByXml(string fileName,string resourceUrl, string controllerName)
        {
            var xDoc = XDocument.Load(fileName);
            var element = xDoc.Element("AuthConfigs");
            if (element == null) return null;

            var info = from m in xDoc.Descendants("AuthConfig")
                       let resourceUrlAttribute = m.Attribute("ResourceUrl")
                       let controllerNameAttribute = m.Attribute("ControllerName")
                where  resourceUrlAttribute != null &&  resourceUrl.Contains(resourceUrlAttribute.Value) && 
                      controllerNameAttribute != null && controllerNameAttribute.Value==controllerName
                       select new AuthConfig
                {
                    ControllerName = m.Attribute("ControllerName")?.Value,
                    ResourceId =Convert.ToInt32(m.Attribute("ResourceId")?.Value),
                    ResourceUrl = m.Attribute("ResourceUrl")?.Value,
                    Roles = m.Attribute("Roles")?.Value,
                };
            return info.FirstOrDefault();
        }
    }
}