using Microsoft.EntityFrameworkCore;
using NightTasker.Common.Core.Persistence;
using NightTasker.Common.Core.Persistence.Repository;
using NightTasker.Identity.Application.ApplicationContracts.Persistence;
using NightTasker.Identity.Domain.Entities.User;

namespace NightTasker.Identity.Infrastructure.Persistence.Repositories;

/// <inheritdoc cref="IUserRefreshTokenRepository"/> 
internal class UserRefreshTokenRepository
    (ApplicationDbSet<UserRefreshToken, Guid> dbSet) : BaseRepository<UserRefreshToken, Guid>(dbSet),
        IUserRefreshTokenRepository
{
    /// <inheritdoc /> 
    public async Task<Guid> CreateToken(Guid userId, CancellationToken cancellationToken)
    {
        var token = new UserRefreshToken { IsValid = true, UserId = userId };
        await Add(token, cancellationToken);
        return token.Id;
    }
    
    /// <inheritdoc /> 
    public Task<UserRefreshToken?> TryGetValidRefreshToken(
        Guid refreshTokenId, 
        bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = Entities;
        if (!trackChanges)
        {
            query = query.AsNoTracking();
        }
        
        return query.FirstOrDefaultAsync(x => x.Id == refreshTokenId && x.IsValid, cancellationToken);
    }
    
    /// <inheritdoc /> 
    public async Task<User?> TryGetUserByRefreshToken(
        Guid refreshTokenId, 
        bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = Entities;
        if (!trackChanges)
        {
            query = query.AsNoTracking();
        }
        
        var refreshToken = await query
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == refreshTokenId, cancellationToken);

        return refreshToken?.User;
    }
}