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
using System.Web;

#endregion Import namespace

namespace WorkData.Code.Helpers
{
    /// <summary>
    /// 页面缓存帮助类
    /// </summary>
    public class PageCacheHelper
    {
        private static System.Web.Caching.Cache _cache = HttpRuntime.Cache;

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <returns></returns>
        public static object SetCache(string key, object val)
        {
            if (_cache[key] == null)
            {
                _cache.Insert(key, val, null, DateTime.Now.AddDays(1), TimeSpan.Zero);
            }

            return _cache[key];
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="minute">缓存的时间长度（单位:分钟）</param>
        /// <returns></returns>
        public static object SetCache(string key, object val, double minute)
        {
            if (_cache[key] == null)
            {
                _cache.Insert(key, val, null, DateTime.Now.AddMinutes(minute), TimeSpan.Zero);
            }

            return _cache[key];
        }

        /// <summary>
        ///  获取数据缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static object GetCache(string key)
        {
            return _cache[key];
        }

        public static T GetCache<T>(string key)
        {
            return (T)_cache[key];
        }

        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        /// <param name="key">要移除缓存的键</param>
        public static void RemoveCache(string key)
        {
            if (_cache[key] != null)
            {
                _cache.Remove(key);
            }
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            var cacheEnum = _cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                _cache.Remove(cacheEnum.Key.ToString());
            }
        }
    }
}