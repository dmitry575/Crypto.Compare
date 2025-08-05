using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Crypto.Compare.DataAccess;

namespace Crypto.Compare.Services.Tests
{
    public static class Crypto.CompareContextTestsHelper
    {
        public static Crypto.CompareContext GetMemoryContext()
        {
            var options = new DbContextOptionsBuilder<Crypto.CompareContext>()
                //.UseSqlite(conn)
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase_" + Guid.NewGuid())
                // don't raise the error warning us that the in memory db doesn't support transactions
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            var d = new Crypto.CompareContext(options);
            if (!d.Database.IsInMemory())
            {
                d.Database.EnsureCreated();
            }

            return d;
        }
    }
}
