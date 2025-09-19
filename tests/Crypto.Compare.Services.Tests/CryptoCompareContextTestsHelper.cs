using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Crypto.Compare.DataAccess;

namespace Crypto.Compare.Services.Tests
{
    public static class CryptoCompareContextTestsHelper
    {
        public static CryptoCompareContext GetMemoryContext()
        {
            var options = new DbContextOptionsBuilder<CryptoCompareContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase_" + Guid.NewGuid())
                // don't raise the error warning us that the in memory db doesn't support transactions
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            var d = new CryptoCompareContext(options);
            if (!d.Database.IsInMemory())
            {
                d.Database.EnsureCreated();
            }

            return d;
        }
    }
}