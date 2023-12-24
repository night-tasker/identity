using NightTasker.Common.Core.Identity.Contracts;
using NightTasker.Common.Core.Persistence;
using NightTasker.Identity.Domain.Entities.User;
using NightTasker.Identity.Infrastructure.Persistence;
using NightTasker.Identity.Infrastructure.Persistence.Contracts;

namespace NightTasker.Identity.Presentation.WebApi.Implementations;

public class ApplicationDbAccessor : IApplicationDbAccessor
{
    private readonly ApplicationDbContext _dbContext;

    private readonly IIdentityService _identityService;

    public ApplicationDbAccessor(ApplicationDbContext dbContext,
        IIdentityService identityService)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        UserRefreshTokens = new(dbContext, GetUserRefreshTokensQuery(dbContext.Set<UserRefreshToken>()));
        Users = new (dbContext, GetUsersQuery(dbContext.Set<User>()));
    }

    /// <inheritdoc />
    public ApplicationDbSet<UserRefreshToken, Guid> UserRefreshTokens { get; }
    
    /// <inheritdoc />
    public ApplicationDbSet<User, Guid> Users { get; }
    
    /// <inheritdoc />
    public Task SaveChanges(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    private IQueryable<UserRefreshToken> GetUserRefreshTokensQuery(IQueryable<UserRefreshToken> query)
    {
        return query;
    }

    private IQueryable<User> GetUsersQuery(IQueryable<User> query)
    {
        if (_identityService.IsAuthenticated)
        {
            var currentUserId = _identityService.CurrentUserId!.Value;
            return query.Where(x => x.Id == currentUserId);
        }

        return EmptyQuery(query);
    }

    private IQueryable<T> EmptyQuery<T>(IQueryable<T> query) => Enumerable.Empty<T>().AsQueryable();
}