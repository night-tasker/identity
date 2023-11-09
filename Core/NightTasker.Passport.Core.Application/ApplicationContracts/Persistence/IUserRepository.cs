using NightTasker.Passport.Domain.Entities.User;

namespace NightTasker.Passport.Application.ApplicationContracts.Persistence;

public interface IUserRepository : IRepository<User, Guid>
{
    
}