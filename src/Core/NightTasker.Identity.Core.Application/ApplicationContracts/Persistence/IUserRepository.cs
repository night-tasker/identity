using NightTasker.Common.Core.Persistence.Repository;
using NightTasker.Identity.Domain.Entities.User;

namespace NightTasker.Identity.Application.ApplicationContracts.Persistence;

/// <summary>
/// Репозиторий пользователей.
/// </summary>
public interface IUserRepository : IRepository<User, Guid>
{
    
}