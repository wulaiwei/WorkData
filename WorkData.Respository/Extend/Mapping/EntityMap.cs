using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Mapping
{
    /// <summary>
    /// A class that defines entity mapping.
    /// </summary>
    [DebuggerDisplay("Table: {TableName}")]
    public class EntityMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityMap" /> class.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        public EntityMap(Type entityType)
        {
            EntityType = entityType;
            KeyMaps = new List<PropertyMap>();
            PropertyMaps = new List<PropertyMap>();
        }

        /// <summary>
        /// Gets or sets the conceptual model EntitySet.
        /// </summary>
        public EntitySet ModelSet { get; set; }

        /// <summary>
        /// Gets or sets the store model EntitySet.
        /// </summary>
        public EntitySet StoreSet { get; set; }

        /// <summary>
        /// Gets or sets the conceptual model EntityType.
        /// </summary>
        public EntityType ModelType { get; set; }

        /// <summary>
        /// Gets or sets the store model EntityType.
        /// </summary>
        public EntityType StoreType { get; set; }

        /// <summary>
        /// Gets the type of the entity.
        /// </summary>
        public Type EntityType { get; }

        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName { get; set; }

        /// <summary>
        /// Gets the property maps.
        /// </summary>
        public List<PropertyMap> PropertyMaps { get; }

        /// <summary>
        /// Gets the key maps.
        /// </summary>
        public List<PropertyMap> KeyMaps { get; }
    }
}