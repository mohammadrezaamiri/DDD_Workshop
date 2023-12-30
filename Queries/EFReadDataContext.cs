using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence;

namespace Queries;

public class EFReadDataContext : DbContext
{
    public EFReadDataContext()
        : base(new DbContextOptionsBuilder<EFReadDataContext>()
            .UseSqlServer(
                "server=.;database=Bank;Trusted_Connection=True;Encrypt=false;TrustServerCertificate=true;")
            .Options)
    {
    }
    
    private EFReadDataContext(DbContextOptions<EFReadDataContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFWriteDataContext).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFReadDataContext).Assembly);
    }

    public override ChangeTracker ChangeTracker
    {
        get
        {
            var tracker = base.ChangeTracker;
            tracker.AutoDetectChangesEnabled = false;
            tracker.LazyLoadingEnabled = false;
            tracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return tracker;
        }
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        throw new InvalidOperationException(
            $"can't call 'SaveChanges' method on read " +
            $"only '{nameof(EFReadDataContext)}' DbContext.");
    }
    public override Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess, 
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new InvalidOperationException(
            $"can't call 'SaveChangesAsync' " +
            $"method on read only '{nameof(EFReadDataContext)}' DbContext.");
    }
}