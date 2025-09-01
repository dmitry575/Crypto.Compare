using Crypto.Compare.Data;
using Microsoft.EntityFrameworkCore;

namespace Crypto.Compare.DataAccess;

public class CryptoCompareContext : DbContext
{
    public CryptoCompareContext(DbContextOptions<CryptoCompareContext> options)
        : base(options)
    {
    }

    public DbSet<SymbolProvider> SymbolProviders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableDetailedErrors();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}