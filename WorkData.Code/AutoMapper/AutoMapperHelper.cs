using AutoMapper;
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
        /// IQueryable实体映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TS"></typeparam>
        /// <param name="sources"></param>
        /// <returns></returns>
        public static IQueryable<T> Queryable<TS, T>(IQueryable<TS> sources) where T : class, new() where TS : class, new()
        {
            return sources == null ? null : Mapper.Map<IQueryable<TS>, IQueryable<T>>(sources);
        }

        /// <summary>
        /// Collection实体映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TS"></typeparam>
        /// <param name="sources"></param>
        /// <returns></returns>
        public static ICollection<T> Collection<TS, T>(ICollection<TS> sources) where T : class, new() where TS : class, new()
        {
            return sources == null ? null : Mapper.Map<ICollection<TS>, ICollection<T>>(sources);
        }
    }
}