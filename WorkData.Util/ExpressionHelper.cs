// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Util 
// 文件名：ExpressionHelper.cs
// 创建标识：吴来伟 2016-11-24
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WorkData.Util
{
    public class ExpressionHelper
    {
        /// <summary>
        /// 根据条件数据动态生成或连接条件
        /// </summary>
        /// <typeparam name="TSource">集合项类型</typeparam>
        /// <param name="sourcePropertyName">待比较的集合项属性</param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Expression<Func<TSource, bool>> GenerateCondition<TSource>(string sourcePropertyName,object param)
        {
            var p = Expression.Parameter(typeof(TSource), "p");
            var propertyName = Expression.Property(p, sourcePropertyName);

            var body = Expression.Equal(propertyName, 
                Expression.Constant(param));
            var orExp = Expression.Lambda<Func<TSource, bool>>(body, p);
            return orExp;

        }
    }
}