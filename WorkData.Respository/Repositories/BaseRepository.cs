using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Extensions;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using WorkData.Infrastructure.IRepositories;
using WorkData.Util.Entity;

namespace WorkData.Respository.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        #region 单实体 增刪改操作

        /// <summary>
        /// 附加至上下文
        /// </summary>
        /// <param name="entity"></param>
        public void Attach(T entity)
        {
            _dbSet.Attach(entity);
        }

        public void Unchanged(T entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// 对象赋值
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtoEntity"></param>
        public void CurrentValue(T entity, T dtoEntity)
        {
            var entry = _dbContext.Entry(entity);
            entry.CurrentValues.SetValues(dtoEntity);
        }

        #endregion 单实体 增刪改操作

        #region 批量 增删改操作


        public void BatchDelete(IEnumerable<T> entitys)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities"></param>
        public void BatchAdd(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Remove(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        /// <summary>
        /// 条件批量删除
        /// </summary>
        /// <param name="predicate"></param>
        public virtual void Remove(Expression<Func<T, bool>> predicate)
        {
            _dbSet.Where(predicate).Delete();
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="updateExpression"></param>
        public virtual void Update(Expression<Func<T, T>> updateExpression)
        {
            _dbSet.Update(updateExpression);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        public virtual int Update(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updateExpression)
        {
            var queryAble = _dbSet.Where(predicate);
            var updateCount = queryAble.Update(updateExpression);

            return updateCount;
        }

        #endregion 批量 增删改操作

        #region 查询
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).Any();
        }

        /// <summary>
        /// 查询By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T Get(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).FirstOrDefault();
        }

        /// <summary>
        /// 查询实体（拉取指定内容）
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeName"></param>
        /// <returns></returns>
        public T Get(Expression<Func<T, bool>> where, string includeName)
        {
            return _dbSet.Where(where)
                .Include(includeName)
                .FirstOrDefault();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> Query()
        {
            return _dbSet.AsNoTracking();
        }

        /// <summary>
        /// 查询（条件）
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IQueryable<T> Query(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).AsNoTracking();
        }

        /// <summary>
        /// 查询  +  延迟加载
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeName"></param>
        /// <returns></returns>
        public IQueryable<T> Query(Expression<Func<T, bool>> @where, string includeName)
        {
            return _dbSet.Where(where).Include(includeName).AsNoTracking();
        }


        /// <summary>
        /// 查询  （条件+排序）
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public IQueryable<T> Query(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderBy)
        {
            return _dbSet.Where(where).OrderBy(orderBy).AsNoTracking();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageEntity"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public IQueryable<T> Page(PageEntity pageEntity, Expression<Func<T, bool>> where)
        {
            var queryable = _dbSet.Where(where).AsQueryable().AsNoTracking();
            pageEntity.Records = queryable.Count();

            var infoList = OrderByIQueryable(queryable, pageEntity.Sidx, pageEntity.Sord)
                .Skip((pageEntity.PageIndex - 1) * pageEntity.PageSize)
                .Take(pageEntity.PageSize);

            return infoList;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="orderByStr"></param>
        /// <param name="orderByMode"></param>
        /// <returns></returns>
        private static IQueryable<T> OrderByIQueryable<T>(IQueryable<T> queryable, string orderByStr, string orderByMode)
        {
            MethodCallExpression resultExp = null;
            var sidxSplit = orderByStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var field in sidxSplit)
            {
                var orderByPart = Regex.Replace(field, @"\s+", " ");
                var orderArray = orderByPart.Split(' ');
                var orderField = orderArray[0];
                var isAsc = orderByMode.ToUpper() == "ASC";
                if (orderArray.Length == 2)
                {
                    isAsc = orderArray[1].ToUpper() == "ASC";
                }
                var orderByMethodName = isAsc ? "OrderBy" : "OrderByDescending";
                var entityType = typeof(T);
                var queryableType = typeof(Queryable);
                var parameter = Expression.Parameter(entityType, "t");
                var property = entityType.GetProperty(orderField);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExpression = Expression.Lambda(propertyAccess, parameter);
                var typeArguments = new[] { entityType, property.PropertyType };
                var arguments = new[] {
                    queryable.Expression,
                    Expression.Quote(orderByExpression)
                };
                resultExp = Expression.Call(queryableType, orderByMethodName, typeArguments, arguments);
            }


            return queryable.Provider.CreateQuery<T>(resultExp);
        }

        #endregion
    }
}