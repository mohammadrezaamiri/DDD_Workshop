using MessageBus;

namespace Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly EFWriteDataContext _dbContext;

    public UnitOfWork(EFWriteDataContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void Start()
    {
        _dbContext.Database.BeginTransaction();
    }

    public void Finish()
    {
        _dbContext.SaveChanges();
        _dbContext.Database.CommitTransaction();
    }

    public void Rollback()
    {
        _dbContext.Database.RollbackTransaction();
    }
}