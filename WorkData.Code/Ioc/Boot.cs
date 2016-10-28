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
using System;

#endregion Import namespace

namespace WorkData.Code.Ioc
{
    /// <summary>
    /// 引导程序
    /// </summary>
    public static class Boot
    {
        private static volatile bool _isInit;
        private static object sync = new object();

        /// <summary>
        ///启动集成框架
        /// </summary>
        [STAThread]
        public static void Start()
        {
            lock (sync)
            {
                if (_isInit) return;
                var builder = new ContainerBuilder();

                Ioc.Container = builder.Build();
                _isInit = true;
            }
        }
    }
}