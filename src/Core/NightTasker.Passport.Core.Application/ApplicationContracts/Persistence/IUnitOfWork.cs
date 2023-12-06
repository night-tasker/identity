namespace NightTasker.Passport.Application.ApplicationContracts.Persistence;

/// <summary>
/// Юнит оф ворк.
/// </summary>
public interface IUnitOfWork
{
    /// <inheritdoc cref="IUserRefreshTokenRepository"/>
    IUserRefreshTokenRepository UserRefreshTokenRepository { get; }
    
    /// <inheritdoc cref="IUserRepository"/>
    IUserRepository UserRepository { get; }
    
    /// <summary>
    /// Сохранить изменения.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task SaveChanges(CancellationToken cancellationToken);
}