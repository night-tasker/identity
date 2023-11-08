using NightTasker.Passport.Application.ApplicationContracts.Persistence;

namespace NightTasker.Passport.Infrastructure.Repositories.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        UserRefreshTokenRepository = new UserRefreshTokenRepository(dbContext);
    }

    public IUserRefreshTokenRepository UserRefreshTokenRepository { get; init; }
    
    public Task SaveChanges(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}