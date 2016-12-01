using System;
using System.Web;
using System.Web.Caching;

namespace WorkData.Mvc.Token
{
    /// <summary>
    /// Http运行时缓存管理
    /// </summary>
    internal class HttpCacheManager
    {
        private static readonly Cache _cache = HttpRuntime.Cache;

        /// <summary>
        /// 设置数据缓存(一天之后绝对到期)
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <returns></returns>
        public static object SetCache(string key, object val)
        {
            if (_cache[key] == null)
            {
                _cache.Insert(key, val, null, DateTime.UtcNow.AddDays(1), Cache.NoSlidingExpiration /*禁用可调到期*/);
            }


            return _cache[key];
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="seconds">缓存的时间长度（单位:秒）</param>
        /// <returns></returns>
        public static object SetCache(string key, object val, double seconds)
        {
            if (_cache[key] == null)
            {
                _cache.Insert(key, val, null, Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(seconds));
            }

            return _cache[key];
        }

        /// <summary>
        /// 获取数据缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static object GetCache(string key)
        {
            return _cache[key];
        }

        public static T GetCache<T>(string key)
        {
            return (T) _cache[key];
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