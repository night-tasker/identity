using NightTasker.Passport.Application.ApplicationContracts.Persistence;

namespace NightTasker.Passport.Infrastructure.Repositories.Common;

/// <inheritdoc />
public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    /// <inheritdoc />
    public IUserRefreshTokenRepository UserRefreshTokenRepository { get; } = new UserRefreshTokenRepository(dbContext);

    /// <inheritdoc />
    public IUserRepository UserRepository { get; } = new UserRepository(dbContext);

    /// <inheritdoc />
    public Task SaveChanges(CancellationToken cancellationToken)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}