namespace NightTasker.Passport.Application.ApplicationContracts.Persistence;

public interface IUnitOfWork
{
    IUserRefreshTokenRepository UserRefreshTokenRepository { get; init; }

    Task SaveChanges(CancellationToken cancellationToken);
}