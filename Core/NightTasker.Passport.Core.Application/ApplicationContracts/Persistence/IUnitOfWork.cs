namespace NightTasker.Passport.Application.ApplicationContracts.Persistence;

public interface IUnitOfWork
{
    IUserRefreshTokenRepository UserRefreshTokenRepository { get; }
    
    IUserRepository UserRepository { get; }

    Task SaveChanges(CancellationToken cancellationToken);
}