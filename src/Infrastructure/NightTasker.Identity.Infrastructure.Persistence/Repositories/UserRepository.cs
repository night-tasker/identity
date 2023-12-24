using Microsoft.EntityFrameworkCore;
using NightTasker.Common.Core.Persistence;
using NightTasker.Common.Core.Persistence.Repository;
using NightTasker.Identity.Application.ApplicationContracts.Persistence;
using NightTasker.Identity.Domain.Entities.User;

namespace NightTasker.Identity.Infrastructure.Persistence.Repositories;

/// <inheritdoc cref="IUserRepository"/>
internal class UserRepository(ApplicationDbSet<User, Guid> dbSet) : BaseRepository<User, Guid>(dbSet), IUserRepository
{
    /// <inheritdoc />
    public Task<User?> TryGetById(Guid id, CancellationToken cancellationToken)
    {
        return Entities.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}