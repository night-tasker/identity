using NightTasker.Identity.Application.ApplicationContracts.Persistence;
using NightTasker.Identity.Domain.Entities.User;
using NightTasker.Identity.Infrastructure.Persistence.Repositories.Base;

namespace NightTasker.Identity.Infrastructure.Persistence.Repositories;

/// <inheritdoc cref="IUserRepository"/>
internal class UserRepository : BaseRepository<User, Guid>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
        
    }
}