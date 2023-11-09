using NightTasker.Passport.Application.ApplicationContracts.Persistence;
using NightTasker.Passport.Domain.Entities.User;
using NightTasker.Passport.Infrastructure.Repositories.Base;

namespace NightTasker.Passport.Infrastructure.Repositories;

internal class UserRepository : BaseRepository<User, Guid>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
        
    }
}