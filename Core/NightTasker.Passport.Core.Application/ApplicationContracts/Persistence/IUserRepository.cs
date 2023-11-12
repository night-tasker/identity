using NightTasker.Passport.Domain.Entities.User;

namespace NightTasker.Passport.Application.ApplicationContracts.Persistence;

/// <summary>
/// Репозиторий пользователей.
/// </summary>
public interface IUserRepository : IRepository<User, Guid>
{
    
}