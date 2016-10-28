using System.Data.Entity.Batch;
using System.Data.Entity.Mapping;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Entity.Extensions
{
    /// <summary>
    /// An extensions class for batch queries.
    /// </summary>
    public static class BatchExtensions
    {
        /// <summary>
        /// Executes a delete statement using the query to filter the rows to be deleted.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="source">The <see cref="T:IQueryable`1" /> used to generate the where clause for the delete statement.</param>
        /// <returns>The number of row deleted.</returns>
        /// <example>
        /// Delete all users with email domain @test.com.
        /// <code><![CDATA[
        /// var db = new TrackerContext();
        /// string emailDomain = "@test.com";
        /// int count = db.Users.Where(u => u.Email.EndsWith(emailDomain)).Delete();
        /// ]]></code>
        /// </example>
        /// <remarks>
        /// When executing this method, the statement is immediately executed on the database provider
        /// and is not part of the change tracking system.  Also, changes will not be reflected on
        /// any entities that have already been materialized in the current context.
        /// </remarks>
        public static int Delete<TEntity>(this IQueryable<TEntity> source)
            where TEntity : class
        {
            var sourceQuery = source.ToObjectQuery();
            if (sourceQuery == null)
                throw new ArgumentException("The query must be of type ObjectQuery or DbQuery.", "source");

            var objectContext = sourceQuery.Context;
            if (objectContext == null)
                throw new ArgumentException("The ObjectContext for the source query can not be null.", "source");

            var entityMap = sourceQuery.GetEntityMap<TEntity>();
            if (entityMap == null)
                throw new ArgumentException("Could not load the entity mapping information for the query ObjectSet.",
                    "source");

            var runner = ResolveRunner();
            return runner.Delete(objectContext, entityMap, sourceQuery);
        }

        /// <summary>
        /// Executes an update statement using the query to filter the rows to be updated.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="source">The query used to generate the where clause.</param>
        /// <param name="updateExpression">The <see cref="MemberInitExpression" /> used to indicate what is updated.</param>
        /// <returns>The number of row updated.</returns>
        /// <example>
        /// Update all users in the test.com domain to be inactive.
        /// <code><![CDATA[
        /// var db = new TrackerContext();
        /// string emailDomain = "@test.com";
        /// int count = db.Users
        ///   .Where(u => u.Email.EndsWith(emailDomain))
        ///   .Update(u => new User { IsApproved = false, LastActivityDate = DateTime.Now });
        /// ]]></code>
        /// </example>
        /// <remarks>
        /// When executing this method, the statement is immediately executed on the database provider
        /// and is not part of the change tracking system.  Also, changes will not be reflected on
        /// any entities that have already been materialized in the current context.
        /// </remarks>
        public static int Update<TEntity>(
            this IQueryable<TEntity> source,
            Expression<Func<TEntity, TEntity>> updateExpression)
            where TEntity : class
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (updateExpression == null)
                throw new ArgumentNullException("updateExpression");

            var sourceQuery = source.ToObjectQuery();
            if (sourceQuery == null)
                throw new ArgumentException("The query must be of type ObjectQuery or DbQuery.", "source");

            var objectContext = sourceQuery.Context;
            if (objectContext == null)
                throw new ArgumentException("The ObjectContext for the query can not be null.", "source");

            var entityMap = sourceQuery.GetEntityMap<TEntity>();
            if (entityMap == null)
                throw new ArgumentException("Could not load the entity mapping information for the source.", "source");

            var runner = ResolveRunner();
            return runner.Update(objectContext, entityMap, sourceQuery, updateExpression);
        }

        private static IBatchRunner ResolveRunner()
        {
            var provider = Locator.Current.Resolve<IBatchRunner>();
            if (provider == null)
                throw new InvalidOperationException(
                    "Could not resolve the IBatchRunner. Make sure IBatchRunner is registered in the Locator.Current container.");

            return provider;
        }

#if NET45

        /// <summary>
        /// Executes a delete statement asynchronously using the query to filter the rows to be deleted.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="source">The <see cref="T:IQueryable`1" /> used to generate the where clause for the delete statement.</param>
        /// <returns>The number of row deleted.</returns>
        /// <example>
        /// Delete all users with email domain @test.com.
        /// <code><![CDATA[
        /// var db = new TrackerContext();
        /// string emailDomain = "@test.com";
        /// int count = db.Users.Where(u => u.Email.EndsWith(emailDomain)).Delete();
        /// ]]></code>
        /// </example>
        /// <remarks>
        /// When executing this method, the statement is immediately executed on the database provider
        /// and is not part of the change tracking system.  Also, changes will not be reflected on
        /// any entities that have already been materialized in the current context.
        /// </remarks>
        public static Task<int> DeleteAsync<TEntity>(this IQueryable<TEntity> source)
            where TEntity : class
        {
            var sourceQuery = source.ToObjectQuery();
            if (sourceQuery == null)
                throw new ArgumentException("The query must be of type ObjectQuery or DbQuery.", "source");

            var objectContext = sourceQuery.Context;
            if (objectContext == null)
                throw new ArgumentException("The ObjectContext for the source query can not be null.", "source");

            var entityMap = sourceQuery.GetEntityMap<TEntity>();
            if (entityMap == null)
                throw new ArgumentException("Could not load the entity mapping information for the query ObjectSet.",
                    "source");

            var runner = ResolveRunner();
            return runner.DeleteAsync(objectContext, entityMap, sourceQuery);
        }

#endif

#if NET45

        /// <summary>
        /// Executes an update statement asynchronously using the query to filter the rows to be updated.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="source">The query used to generate the where clause.</param>
        /// <param name="updateExpression">The <see cref="MemberInitExpression" /> used to indicate what is updated.</param>
        /// <returns>The number of row updated.</returns>
        /// <example>
        /// Update all users in the test.com domain to be inactive.
        /// <code><![CDATA[
        /// var db = new TrackerContext();
        /// string emailDomain = "@test.com";
        /// int count = db.Users
        ///   .Where(u => u.Email.EndsWith(emailDomain))
        ///   .Update(u => new User { IsApproved = false, LastActivityDate = DateTime.Now });
        /// ]]></code>
        /// </example>
        /// <remarks>
        /// When executing this method, the statement is immediately executed on the database provider
        /// and is not part of the change tracking system.  Also, changes will not be reflected on
        /// any entities that have already been materialized in the current context.
        /// </remarks>
        public static Task<int> UpdateAsync<TEntity>(
            this IQueryable<TEntity> source,
            Expression<Func<TEntity, TEntity>> updateExpression)
            where TEntity : class
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (updateExpression == null)
                throw new ArgumentNullException("updateExpression");

            var sourceQuery = source.ToObjectQuery();
            if (sourceQuery == null)
                throw new ArgumentException("The query must be of type ObjectQuery or DbQuery.", "source");

            var objectContext = sourceQuery.Context;
            if (objectContext == null)
                throw new ArgumentException("The ObjectContext for the query can not be null.", "source");

            var entityMap = sourceQuery.GetEntityMap<TEntity>();
            if (entityMap == null)
                throw new ArgumentException("Could not load the entity mapping information for the source.", "source");

            var runner = ResolveRunner();
            return runner.UpdateAsync(objectContext, entityMap, sourceQuery, updateExpression);
        }

#endif
    }
}