// -----------------------------------------------------------------------
//  <copyright file="MultiDatabaseHiLoGenerator.cs" company="Hibernating Rhinos LTD">
//      Copyright (c) Hibernating Rhinos LTD. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
using System.Collections.Concurrent;
using Raven.NewClient.Abstractions.Data;
using Raven.NewClient.Client.Connection;

namespace Raven.NewClient.Client.Document
{
    public class MultiDatabaseHiLoGenerator
    {
        private readonly int capacity;

        private readonly ConcurrentDictionary<string, MultiTypeHiLoKeyGenerator> generators =
            new ConcurrentDictionary<string, MultiTypeHiLoKeyGenerator>();

        public MultiDatabaseHiLoGenerator(int capacity)
        {
            this.capacity = capacity;
        }

        public string GenerateDocumentKey(string dbName, DocumentConvention conventions, object entity)
        {
            var multiTypeHiLoKeyGenerator = generators.GetOrAdd(dbName ?? Constants.SystemDatabase, s => new MultiTypeHiLoKeyGenerator(capacity));
            return multiTypeHiLoKeyGenerator.GenerateDocumentKey(conventions, entity);
        }
    }
}