using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using WorkData.EF.Domain;
using WorkData.Infrastructure.IDbContexts;
using WorkData.Infrastructure.ITransactions;
using WorkData.Respository.Transactions;

namespace WorkData.Respository.DbContexts
{
    public class EfDbContext : IDbContext
    {
        private readonly DbContext _dbContext;

        public EfDbContext(DbContext dbContext)
        {
            _dbContext = dbContext;
            //_dbSet = _dbContext.Set();
        }

        public void Add<T>(T entity) where T : class
        {
              _dbContext.Entry(entity).State = EntityState.Added;
        }

        public void Delete<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public void Update<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public T Get<T>(Expression<Func<T, bool>> where) where T : class
        {
            return _dbContext.Set<T>().Where(where).AsNoTracking().FirstOrDefault();
        }

        public T Get<T>(int id) where T : class
        {
            var dbSet = _dbContext.Set<T>();
            return dbSet.Find(id);
        }

        public IQueryable<T> Query<T>() where T : class
        {
            var dbSet = _dbContext.Set<T>();
            return dbSet.AsQueryable();
        }

        public IQueryable<T> Query<T>(Expression<Func<T, bool>> where) where T : class
        {
            var dbSet = _dbContext.Set<T>();
            return dbSet.Where(where).AsQueryable();
        }

        public void BatchDelete<T>(IEnumerable<T> entitys) where T : class
        {
            throw new NotImplementedException();
        }

        public void BatchAdd<T>(IEnumerable<T> entitys) where T : class
        {
            throw new NotImplementedException();
        }

        #region 数据操作
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void Unchanged<T>(T entity) where T : class
        {
            _dbContext.Entry(entity);
        }

        public void Attach<T>(T entity) where T : class
        {
            RemoveHoldingEntityInContext(entity);
            var dbset = _dbContext.Set(typeof(T));
            dbset.Attach(entity);
        }

        //用于监测Context中的Entity是否存在，如果存在，将其Detach，防止出现问题。
        public bool RemoveHoldingEntityInContext<T>(T entity) where T : class
        {
            var objContext = ((IObjectContextAdapter)_dbContext).ObjectContext;
            var objSet = objContext.CreateObjectSet<T>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);

            if (exists)
            {
                objContext.Detach(foundEntity);
            }

            return (exists);
        }

        public ITransaction GetTransaction(IsolationLevel level)
        {
            var trans = _dbContext.Database.BeginTransaction(level);
            return new EfTransaction(trans);
        }
        #endregion
    }
}
