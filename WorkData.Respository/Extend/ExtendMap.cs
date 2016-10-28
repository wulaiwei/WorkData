#region Apache License Version 2.0

// ---------------------------------------------------------------------------
//  Copyright 2016  The LightWork Project
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
// ---------------------------------------------------------------------------

#endregion Apache License Version 2.0

#region Import namespace

using System;
using System.Data.Entity;
using System.Data.Entity.Mapping;

#endregion Import namespace

// ReSharper disable CheckNamespace

namespace LightWork.Data
{
    public static class ExtendMap
    {
        public static EntityMap GetEntityMap(this Type entityType, DbContext context)
        {
            var metadataResolver = new MetadataMappingProvider();
            var entityMap = metadataResolver.GetEntityMap(entityType, context);

            return entityMap;
        }

        public static EntityMap GetEntityMap<T>(this DbContext context) where T : class
        {
            var metadataResolver = new MetadataMappingProvider();
            var entityType = typeof(T);
            var entityMap = metadataResolver.GetEntityMap(entityType, context);

            return entityMap;
        }
    }
}