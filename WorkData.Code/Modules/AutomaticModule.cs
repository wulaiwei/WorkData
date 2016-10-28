#region Apache License Version 2.0

// ---------------------------------------------------------------------------
//  Copyright 2016  The LightWork Project
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
// ---------------------------------------------------------------------------

#endregion Apache License Version 2.0

#region Import namespace

using Autofac;
using LightWork.Helpers;
using System.Linq;

#endregion Import namespace

namespace WorkData.Code.Modules
{
    /// <summary>
    /// 用于Autofac 自动装配的模块
    /// </summary>
    public class AutomaticModule : Module
    { }

    public class LightWorkManagedModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = AssemblyHelper.GetAssemblies().ToArray();
            builder.RegisterAssemblyModules<AutomaticModule>(assemblies);
        }
    }
}