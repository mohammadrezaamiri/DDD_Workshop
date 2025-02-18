using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Persistence;

public class EFWriteDataContext : DbContext
{
    public EFWriteDataContext()
        : base(new DbContextOptionsBuilder<EFWriteDataContext>()
            .UseSqlServer(
                "server=.;database=Bank;Trusted_Connection=True;Encrypt=false;TrustServerCertificate=true;")
            .Options)
    {
    }
    
    private EFWriteDataContext(DbContextOptions<EFWriteDataContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFWriteDataContext).Assembly);
    }

    public override ChangeTracker ChangeTracker
    {
        get
        {
            var tracker = base.ChangeTracker;
            tracker.LazyLoadingEnabled = false;
            tracker.AutoDetectChangesEnabled = true;
            tracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            return tracker;
        }
    }
}