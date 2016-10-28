// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。
// 项目名：UnitWork.Repository
// 文件名：EfUnitWork.cs
// 创建标识：laiwei wu  2016-08-29 16:42
// 创建描述：
//
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using WorkData.Infrastructure.IRepositories;
using WorkData.Infrastructure.ITransactions;
using WorkData.Infrastructure.IUnitOfWorks;
using WorkData.Respository.Repositories;
using WorkData.Respository.Transactions;

namespace WorkData.Respository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _db;

        public UnitOfWork(DbContext context)
        {
            _db = context;
        }

        public Dictionary<Type, object> Repositories = new Dictionary<Type, object>();

        public IRepository<T> Repository<T>() where T : class
        {
            if (Repositories.Keys.Contains(typeof(T)) == true)
            {
                return Repositories[typeof(T)] as IRepository<T>;
            }
            IRepository<T> repo = new BaseRepository<T>(_db);
            Repositories.Add(typeof(T), repo);
            return repo;
        }

        #region 事务管理

        public void Commit()
        {
            using (var transaction = BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _db.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.RollBack();
                    throw;
                }
            }
        }

        public ITransaction BeginTransaction(IsolationLevel level)
        {
            var trans = _db.Database.BeginTransaction(level);
            return new EfTransaction(trans);
        }

        #endregion 事务管理

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}