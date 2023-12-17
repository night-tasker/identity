using NightTasker.Common.Core.Persistence.Repository;
using NightTasker.Identity.Application.ApplicationContracts.Persistence;
using NightTasker.Identity.Domain.Entities.User;

namespace NightTasker.Identity.Infrastructure.Persistence.Repositories;

/// <inheritdoc cref="IUserRepository"/>
internal class UserRepository : BaseRepository<User, Guid, ApplicationDbContext>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
        
    }
}