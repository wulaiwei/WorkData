using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkData.Code.AutoMapper
{
    public class AutoMapperHelper
    {
        /// <summary>
        /// 单实体映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TS"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Signle<TS, T>(TS source) where T : class, new() where TS : class, new()
        {
            return source == null ? null : Mapper.Map<TS, T>(source);
        }

        /// <summary>
        /// 单实体映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TS"></typeparam>
        /// <param name="source"></param>
        /// <param name="opt"></param>
        /// <returns></returns>
        public static T Signle<TS, T>(TS source, Action<TS, T> opt) where T : class, new() where TS : class, new()
        {
            return source == null ? null : Mapper.Map<TS, T>(source);
        }

        /// <summary>
        /// IList实体映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TS"></typeparam>
        /// <param name="sources"></param>
        /// <returns></returns>
        public static IList<T> List<TS, T>(IList<TS> sources) where T : class, new() where TS : class, new()
        {
            return sources == null ? null : Mapper.Map<IList<TS>, IList<T>>(sources);
        }

        /// <summary>
        /// IEnumerable实体映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TS"></typeparam>
        /// <param name="sources"></param>
        /// <returns></returns>
        public static IEnumerable<T> Enumerable<TS, T>(IEnumerable<TS> sources) where T : class, new() where TS : class, new()
        {
            return sources == null ? null : Mapper.Map<IEnumerable<TS>, IEnumerable<T>>(sources);
        }
    }
}