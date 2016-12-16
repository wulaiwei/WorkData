// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Util 
// 文件名：AssemblyHelper.cs
// 创建标识：吴来伟 2016-12-06
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using WorkData.Util.Entity;

namespace WorkData.Util
{
    public class AssemblyHelper
    {
        /// <summary>
        /// 加载指定程序集Action
        /// </summary>
        /// <param name="path"></param>
        public static List<AssemblyControllerResult> LoadAction(string path)
        {
            //无需加载的控制器列表
            var array = new string[] { "DefaultController", "BaseController", "FileController",
                "ManagerController","LoginController"};
            var assembly = Assembly.Load("WorkData.Web");    //加载程序集
            var controllerTypes = new List<Type>();    //创建控制器类型列表
            controllerTypes.AddRange(assembly.GetTypes().Where(type => typeof(IController).IsAssignableFrom(type)
            && !array.Contains(type.Name)));

            var assemblyControllerResultList = new List<AssemblyControllerResult>();

            //获取程序集下所有的类，通过Linq筛选继承IController类的所有类型
            foreach (var controllerType in controllerTypes)
            {
                var info = new AssemblyControllerResult
                {
                    ControllerName = controllerType.Name.Replace("Controller", "")
                };
                var actionList = controllerType.GetMethods().Where(method =>
                method.CustomAttributes.Any(x => x.AttributeType == typeof(ActionDescriptionAttribute)));

                foreach (var action in actionList)
                {
                    var actionDescriptionAttribute = action.GetCustomAttribute(typeof(ActionDescriptionAttribute)) as ActionDescriptionAttribute;
                    if (actionDescriptionAttribute == null) continue;

                    var actionMethodAttribute = action.GetCustomAttribute(typeof(ActionMethodSelectorAttribute)) as ActionMethodSelectorAttribute;

                    var name = actionMethodAttribute?.GetType().Name;
                    var dataInfo = new ActionDescriptionAttribute
                    {
                        Action = actionDescriptionAttribute.Action,
                        Name = actionDescriptionAttribute.Name,
                        Method = name == null || name == "HttpGetAttribute" ? "Get" : "Post"
                    };
                    info.Actions.Add(dataInfo);
                }
                assemblyControllerResultList.Add(info);
            }

            return assemblyControllerResultList;
        }
    }
}