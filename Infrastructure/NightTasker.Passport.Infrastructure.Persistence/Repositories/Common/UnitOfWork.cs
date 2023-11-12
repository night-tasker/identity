using NightTasker.Passport.Application.ApplicationContracts.Persistence;

namespace NightTasker.Passport.Infrastructure.Repositories.Common;

/// <inheritdoc />
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        UserRefreshTokenRepository = new UserRefreshTokenRepository(dbContext);
        UserRepository = new UserRepository(dbContext);
    }

    
    /// <inheritdoc />
    public IUserRefreshTokenRepository UserRefreshTokenRepository { get; }
    
    /// <inheritdoc />
    public IUserRepository UserRepository { get; }

    /// <inheritdoc />
    public Task SaveChanges(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}