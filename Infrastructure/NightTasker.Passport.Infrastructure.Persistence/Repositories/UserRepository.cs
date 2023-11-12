using NightTasker.Passport.Application.ApplicationContracts.Persistence;
using NightTasker.Passport.Domain.Entities.User;
using NightTasker.Passport.Infrastructure.Repositories.Base;

namespace NightTasker.Passport.Infrastructure.Repositories;

/// <inheritdoc cref="IUserRepository"/>
internal class UserRepository : BaseRepository<User, Guid>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
        
    }
}