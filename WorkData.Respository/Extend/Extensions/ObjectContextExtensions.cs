using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace System.Data.Entity.Extensions
{
    /// <summary>
    /// Extensions methods for <see cref="ObjectContext" /> and <see cref="DbContext" />.
    /// </summary>
    public static class ObjectContextExtensions
    {
        /// <summary>
        /// Starts a database transaction from the database provider connection.
        /// </summary>
        /// <param name="context">The <see cref="ObjectContext" /> to get the database connection from.</param>
        /// <param name="isolationLevel">Specifies the isolation level for the transaction.</param>
        /// <returns>
        /// An object representing the new transaction.
        /// </returns>
        public static DbTransaction BeginTransaction(this ObjectContext context,
            IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (context.Connection.State != ConnectionState.Open)
                context.Connection.Open();

            return context.Connection.BeginTransaction(isolationLevel);
        }

        internal static EntitySetBase GetEntitySet<TEntity>(this ObjectContext context)
        {
            var name = typeof(TEntity).FullName;
            return GetEntitySet(context, name);
        }

        internal static EntitySetBase GetEntitySet(this ObjectContext context, string elementTypeName)
        {
            var container = context.MetadataWorkspace.GetEntityContainer(context.DefaultContainerName, DataSpace.CSpace);
            return container.BaseEntitySets.FirstOrDefault(item => item.ElementType.FullName.Equals(elementTypeName));
        }
    }
}