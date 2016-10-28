// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：UnitOfWork.Infrastructure 
// 文件名：IDbContext.cs
// 创建标识：  2016-08-30 11:20
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using WorkData.Infrastructure.ITransactions;

namespace WorkData.Infrastructure.IDbContexts
{
    public interface IDbContext
    {
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        T Get<T>(int id) where T : class;
        T Get<T>(Expression<Func<T, bool>> where) where T : class;


       IQueryable<T> Query<T>() where T : class;

        IQueryable<T> Query<T>(Expression<Func<T, bool>> where) where T : class;

        void BatchDelete<T>(IEnumerable<T> entitys) where T : class;

        void BatchAdd<T>(IEnumerable<T> entitys) where T : class;


        void SaveChanges();

        void Dispose();

        void Unchanged<T>(T entity) where T : class;

        void Attach<T>(T entity) where T : class;

        ITransaction GetTransaction(IsolationLevel level);
    }
}