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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

#endregion Import namespace

namespace LightWork.Helpers
{
    public static class AssemblyHelper
    {
        public static IEnumerable<Assembly> GetAssemblies()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (HttpContext.Current != null)
            {
                baseDirectory = $"{baseDirectory}bin";
            }

            var directoryInfo = new DirectoryInfo(baseDirectory);
            var dlls = directoryInfo.GetFiles("*.dll", SearchOption.AllDirectories);
            var exes = directoryInfo.GetFiles("*.exe", SearchOption.AllDirectories);
            var assemblies = dlls.Union(exes).Select(file => Assembly.LoadFrom(file.FullName));

            return assemblies;
        }
    }
}