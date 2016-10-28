using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Caching
{
    /// <summary>
    /// A class representing a unique key for cache items.
    /// </summary>
    public class CacheKey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheKey" /> class.
        /// </summary>
        /// <param name="key">The key for a cache item.</param>
        public CacheKey(string key)
            : this(key, Enumerable.Empty<string>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheKey" /> class.
        /// </summary>
        /// <param name="key">The key for a cache item.</param>
        /// <param name="tags">The tags for the cache item.</param>
        public CacheKey(string key, IEnumerable<string> tags)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (tags == null)
                throw new ArgumentNullException("tags");

            Key = key;

            var cacheTags = tags.Select(k => new CacheTag(k));
            Tags = new HashSet<CacheTag>(cacheTags);
        }

        /// <summary>
        /// Gets the key for a cached item.
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Gets the tags for a cached item.
        /// </summary>
        public HashSet<CacheTag> Tags { get; }
    }
}