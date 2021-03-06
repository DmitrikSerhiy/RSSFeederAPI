﻿using Feeder.DAL.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freeder.BLL.CacheManagers
{
    public class CollectionCacheManager : CacheManager<Collection>
    {
        internal override string Determinant { get; set; } = "Collection";

        public CollectionCacheManager(IMemoryCache MemoryCache) : base(MemoryCache)
        {}

        internal override Collection Get(string collectionName, bool withIncludes)
        {
            if (withIncludes) Determinant += "WithIncludes";

            if (cache.TryGetValue(Determinant + collectionName, out Collection collection))
                return collection;
            return null;
        }

        internal override Collection Set(string collectionName, Collection collection, bool withIncludes)
        {
            if (withIncludes) Determinant += "WithIncludes";

            return cache.Set(Determinant + collectionName, collection,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(casheExpiration));
        }

        internal override void Remove(string collectionName, bool withIncludes)
        {
            if (withIncludes) Determinant += "WithIncludes";
            cache.Remove(Determinant + collectionName);
        }
    }
}
