using NightTasker.Identity.Application.ApplicationContracts.Persistence;
using NightTasker.Identity.Infrastructure.Persistence.Contracts;

namespace NightTasker.Identity.Infrastructure.Persistence.Repositories.Common;

/// <inheritdoc />
public class UnitOfWork(IApplicationDbAccessor applicationDbAccessor) : IUnitOfWork
{
    private readonly IApplicationDbAccessor _applicationDbAccessor =
        applicationDbAccessor ?? throw new ArgumentNullException(nameof(applicationDbAccessor));
    
    /// <inheritdoc />
    public IUserRefreshTokenRepository UserRefreshTokenRepository { get; } =
        new UserRefreshTokenRepository(applicationDbAccessor.UserRefreshTokens);

    /// <inheritdoc />
    public IUserRepository UserRepository { get; } = new UserRepository(applicationDbAccessor.Users);

    /// <inheritdoc />
    public Task SaveChanges(CancellationToken cancellationToken)
    {
        return _applicationDbAccessor.SaveChanges(cancellationToken);
    }
}