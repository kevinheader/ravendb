// -----------------------------------------------------------------------
//  <copyright file="DatabaseOpenedCount.cs" company="Hibernating Rhinos LTD">
//      Copyright (c) Hibernating Rhinos LTD. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
using System.Linq;

using Lextm.SharpSnmpLib;

using Raven.Abstractions.Data;
using Raven.Database.Server.Tenancy;

namespace Raven.Database.Plugins.Builtins.Monitoring.Snmp.Objects.Database.Statistics
{
    public class DatabaseNumberOfErrorIndexes : DatabaseScalarObjectBase<Integer32>
    {
        public DatabaseNumberOfErrorIndexes(string databaseName, DatabasesLandlord landlord, int index)
            : base(databaseName, landlord, "5.2.{0}.5.7", index)
        {
        }

        protected override Integer32 GetData(DocumentDatabase database)
        {
            var count = database
                .IndexStorage
                .Indexes
                .Select(indexId => database.IndexStorage.GetIndexInstance(indexId))
                .Where(instance => instance != null)
                .Count(instance => instance.Priority.HasFlag(IndexingPriority.Error));

            return new Integer32(count);
        }
    }
}
