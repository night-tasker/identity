using NightTasker.Passport.Application.ApplicationContracts.Persistence;

namespace NightTasker.Passport.Infrastructure.Repositories.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        UserRefreshTokenRepository = new UserRefreshTokenRepository(dbContext);
        UserRepository = new UserRepository(dbContext);
    }

    public IUserRefreshTokenRepository UserRefreshTokenRepository { get; }
    
    public IUserRepository UserRepository { get; }

    public Task SaveChanges(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}