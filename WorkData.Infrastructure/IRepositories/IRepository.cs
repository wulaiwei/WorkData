// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。
// 项目名：UnitWork.Repository
// 文件名：IRepository.cs
// 创建标识：laiwei wu  2016-08-29 17:10
// 创建描述：
//
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WorkData.Util.Entity;

namespace WorkData.Infrastructure.IRepositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);

        T Get(int id);

        T Get(Expression<Func<T, bool>> where);

        T Get(Expression<Func<T, bool>> where, string includeName);

        IQueryable<T> Query();

        IQueryable<T> Page(PageEntity pageEntity,Expression<Func<T, bool>> where);

        IQueryable<T> Query(Expression<Func<T, bool>> where);

        IQueryable<T> Query(Expression<Func<T, bool>> where, string includeName);

        IQueryable<T> Query(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderBy);

        void BatchDelete(IEnumerable<T> entitys);

        void BatchAdd(IEnumerable<T> entitys);

        void CurrentValue(T entity, T dtoEntity);

        void Attach(T entity);

        void Unchanged(T entity);

    }
}