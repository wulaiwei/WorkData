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
using System.Collections;
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
        /// <param name="result"></param>
        /// <param name="fileName"></param>
        public static void CreateAuthConfigXml(List<AuthConfig> list, List<AssemblyControllerResult> result, string fileName)
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
                     new XAttribute("Roles", string.IsNullOrEmpty(p.Roles) ? "" : p.Roles),
                     result.Where(x => x.ControllerName == p.ControllerName).Select(r => new XElement(
                             "Actions",
                              new XAttribute("ControllerName", r.ControllerName),
                              r.Actions.Select(action => new XElement(
                                  "Action",
                                  new XAttribute("ActionName", action.Action),
                                  new XAttribute("Roles", p.Roles),
                                  new XAttribute("Method", action.Method),
                                  new XAttribute("Name", action.Name)
                              ))
                             )
                     ))
                )
            );
            xElement.Save(fileName);
        }

        /// <summary>
        /// 更新权限文件  仅限于新增控制器或修改Action
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="result"></param>
        public static void UpdateConfig(string fileName, List<AssemblyControllerResult> result)
        {
            var xElement = XElement.Load(fileName);
            var infoList = from e in xElement.Elements("AuthConfig")
                           select e;

            foreach (var info in infoList)
            {
                var controllerName = info.Attribute("ControllerName")?.Value;
                if (string.IsNullOrEmpty(controllerName)) continue;
                var assemblyControllerResult = result.FirstOrDefault(x => x.ControllerName == controllerName);

                if (assemblyControllerResult == null) continue;

                var actionList = assemblyControllerResult.Actions;

                foreach (var action in actionList)
                {
                    var actionInfo = from e in info.Elements("Actions").Elements("Action")
                                     let xAttribute = e.Attribute("ActionName")
                                     let mAttribute = e.Attribute("Method")
                                     where xAttribute != null && xAttribute.Value == action.Action
                                     && mAttribute != null && mAttribute.Value == action.Method
                                     select e;
                    if (actionInfo.FirstOrDefault() != null)
                    {
                        info?.SetAttributeValue("Method", action.Method);
                    }
                    else
                    {

                        var xE = new XElement(
                                      "Action",
                                      new XAttribute("ActionName", action.Action),
                                      new XAttribute("Roles", ""),
                                      new XAttribute("Method", action.Method),
                                      new XAttribute("Name", action.Name)
                                      );
                        var firstOrDefault = info.Elements("Actions").FirstOrDefault();
                        firstOrDefault?.Add(xE);
                    }
                }
                var allActionList = actionList.Select(x => x.Action);
                var deleteElementList = from e in info.Elements("Actions").Elements("Action")
                                        let xAttribute = e.Attribute("ActionName")
                                        where xAttribute != null && !allActionList.Contains(xAttribute.Value)
                                        select e;
                deleteElementList.Remove();

            }
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
                           where xAttribute != null && xAttribute.Value == authConfig.ResourceId.ToString()
                           select e;
            var info = infoList.FirstOrDefault();
            info?.SetAttributeValue("ControllerName", authConfig.ControllerName);
            info?.SetAttributeValue("ResourceUrl", authConfig.ResourceUrl);
            if (info != null)
            {
                var actionInfo = info.Elements("Actions").FirstOrDefault();
                var controllerName = actionInfo?.Attribute("ControllerName")?.Value;

                if (controllerName != authConfig.ControllerName)
                {
                    actionInfo?.SetAttributeValue("ControllerName", authConfig.ControllerName);
                    actionInfo?.Elements("Action").Remove();
                }
            }
            xElement.Save(fileName);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="roleCode"></param>
        /// <param name="actionArray"></param>
        public static void UpateActionRolesAuthConfigByXml(string fileName, string roleCode, string[] actionArray)
        {
            var array = new ArrayList();
            var xElement = XElement.Load(fileName);
            foreach (var str in actionArray)
            {
                var info = BusinessHelper.BreakUpOptions(str, ',');
                var resourceId = info.FirstOrDefault();
                var method = info[1];
                var action = info.LastOrDefault();
                if (resourceId != null)
                {
                    array.Add(resourceId);
                    var infoList = from e in xElement.Elements("AuthConfig")
                        let xAttribute = e.Attribute("ResourceId")
                        where xAttribute != null && xAttribute.Value== resourceId
                        select  e;

                    var firstOrDefault = infoList.FirstOrDefault();
                    if (firstOrDefault == null) continue;

                    var actionInfo = from e in firstOrDefault.Elements("Actions").Elements("Action")
                        let xAttribute = e.Attribute("ActionName")
                        let mAttribute = e.Attribute("Method")
                        where xAttribute != null && xAttribute.Value == action &&
                         mAttribute != null && mAttribute.Value == method
                    select e;

                    var item = actionInfo.FirstOrDefault();
                    var actionRoles = item?.Attribute("Roles")?.Value;
                    if (actionRoles != null && actionRoles.Contains(roleCode)) continue;
                    actionRoles = !string.IsNullOrEmpty(actionRoles) ? actionRoles + "," + roleCode : roleCode;

                    item?.SetAttributeValue("Roles", actionRoles);
                }
            }

            var otherInfoList = from e in xElement.Elements("AuthConfig")
                                let xAttribute = e.Attribute("ResourceId")
                                where xAttribute != null && !array.Contains(xAttribute.Value)
                                select e;

            foreach (var info in otherInfoList)
            {

                var actionInfo = from e in info.Elements("Actions").Elements("Action")
                                 let xAttribute = e.Attribute("Roles")
                                 where xAttribute != null && xAttribute.Value == roleCode
                                 select e;
                foreach (var item in actionInfo)
                {
                    var roles = item.Attribute("Roles")?.Value;

                    if (roles == null || !roles.Contains(roleCode)) continue;

                    roles = roles.Replace(roleCode + ",", "").Replace(roleCode, "");
                    item.SetAttributeValue("Roles", roles);
                }
            }

            xElement.Save(fileName);
        }



        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="array"></param>
        /// <param name="roleCode"></param>
        public static void UpateRolesAuthConfigByXml(string fileName, string[] array, string roleCode)
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

                roles = roles.Length > 0 ? roles + "," + roleCode : roleCode;
                info.SetAttributeValue("Roles", roles);
                var resourceId = info.Attribute("ResourceId")?.Value;

            }

            var otherInfoList = from e in xElement.Elements("AuthConfig")
                                let xAttribute = e.Attribute("ResourceId")
                                where xAttribute != null && !array.Contains(xAttribute.Value)
                                select e;

            foreach (var info in otherInfoList)
            {

                var roles = info.Attribute("Roles")?.Value;

                if (roles == null || !roles.Contains(roleCode)) continue;
                var arr = BusinessHelper.BreakUpOptions(roles, ',');

                roles = roles.Replace(roleCode + ",", "").Replace(roleCode, "");
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
                     new XAttribute("Roles", string.IsNullOrEmpty(authConfig.Roles) ? "" : authConfig.Roles),
                     !string.IsNullOrEmpty(authConfig.ResourceUrl) ? new XElement(
                             "Actions",
                              new XAttribute("ControllerName", authConfig.ControllerName)
                     ) : null

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
        /// <param name="action"></param>
        /// <param name="method"></param>
        /// <param name="categoryKey"></param>
        /// <returns></returns>
        public static AuthConfig GetAuthConfigByXml(string fileName, string resourceUrl, string controllerName, string action,string method,string categoryKey)
        {
            var xDoc = XDocument.Load(fileName);
            var element = xDoc.Element("AuthConfigs");
            if (element == null) return null;

            var info =
                xDoc.Descendants("AuthConfig")
                    .Select(m => new {m, controllerNameAttribute = m.Attribute("ControllerName")})
                    .Where(
                        @t => @t.controllerNameAttribute != null && @t.controllerNameAttribute.Value == controllerName)
                    .Select(@t => @t.m)
                ;

            if (!string.IsNullOrEmpty(categoryKey))
            {
                info=info.Select(m => new { m, rAttribute = m.Attribute("ResourceUrl") })
                      .Where(
                        @t => @t.rAttribute != null && @t.rAttribute.Value.Contains("CategoryKey="+categoryKey))
                    .Select(@t => @t.m);
            }


            var xElements = info as XElement[] ?? info.ToArray();
            var dataInfo =
                xElements.Elements("Actions")
                    .Elements("Action")
                    .Select(e => new { e, xAttribute = e.Attribute("ActionName"), mAttribute = e.Attribute("Method") })
                    .Where(
                        @t => @t.xAttribute != null && @t.xAttribute.Value == action 
                        && @t.mAttribute != null && @t.mAttribute.Value.ToUpper() == method
                    )
                    .Select(@t => new AuthConfig
                    {
                        ControllerName = controllerName,
                        Roles = @t.e.Attribute("Roles")?.Value,
                        Action = action
                    });
            return dataInfo.FirstOrDefault();
        }

        /// <summary>
        /// 查询指定数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> GetAuthConfigListByXml(string fileName, string roleCode)
        {
            var xDoc = XDocument.Load(fileName);
            var element = xDoc.Element("AuthConfigs");
            if (element == null) return null;

            var info = from m in xDoc.Descendants("AuthConfig")
                       let controllerNameAttribute = m.Attribute("ControllerName")
                       where
                       controllerNameAttribute != null && controllerNameAttribute.Value.Length > 0
                       select m
                             ;
            var xElements = info as XElement[] ?? info.ToArray();
            var dataInfo =
                xElements.Elements("Actions")
                    .Elements("Action")
                    .Select(e => new
                    {
                        e,
                        xAttribute = e.Attribute("ActionName"),
                        rAttribute = e.Attribute("Roles"),
                        mAttribute = e.Attribute("Method"),
                        nAttribute = e.Attribute("Name"),
                        cAttribute = e.Parent?.Attribute("ControllerName"),
                        kAttribute = e.Parent?.Parent?.Attribute("ResourceId")
                    })
                    .Where(@t => @t.xAttribute != null)
                    .Select(@t =>
                    {
                        var firstOrDefault = xElements.Elements("Actions").FirstOrDefault();
                        if (firstOrDefault != null)
                            return new
                            {
                                ControllerName = @t.cAttribute?.Value,
                                Roles = @t.rAttribute?.Value,
                                Action = @t.xAttribute?.Value,
                                Name = @t.nAttribute?.Value,
                                Checked = BusinessHelper.BreakUpOptions(@t.rAttribute?.Value, ',').Contains(roleCode),
                                ResourceId = @t.kAttribute?.Value,
                                Method=@t.mAttribute?.Value
                            };
                        else
                            return null;
                    });
            return dataInfo;
        }

    }
}