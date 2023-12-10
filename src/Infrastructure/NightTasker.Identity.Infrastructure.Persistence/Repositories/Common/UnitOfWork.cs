using NightTasker.Identity.Application.ApplicationContracts.Persistence;

namespace NightTasker.Identity.Infrastructure.Persistence.Repositories.Common;

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