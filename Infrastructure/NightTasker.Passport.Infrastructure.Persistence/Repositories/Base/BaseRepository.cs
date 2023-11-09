using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NightTasker.Passport.Application.ApplicationContracts.Persistence;
using NightTasker.Passport.Domain.Entities.Common;

namespace NightTasker.Passport.Infrastructure.Repositories.Base;

internal abstract class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity
{
    private readonly ApplicationDbContext _context;
    private DbSet<TEntity> Entities { get; init; }

    protected virtual IQueryable<TEntity> Table => Entities;

    protected virtual IQueryable<TEntity> NoTrackingTable => Entities.AsNoTrackingWithIdentityResolution();
    
    protected BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        Entities = context.Set<TEntity>();
    }
    
    protected virtual async Task<List<TEntity>> ListAll(CancellationToken cancellationToken)
    {
        return await Entities.ToListAsync(cancellationToken);
    }

    protected virtual async Task Add(TEntity entity, CancellationToken cancellationToken)
    { 
        await Entities.AddAsync(entity, cancellationToken);
    }

    protected virtual async Task UpdateByExpression(
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> updateExpression, 
        CancellationToken cancellationToken)
    {
        await Entities.ExecuteUpdateAsync(updateExpression, cancellationToken);
    }

    protected virtual async Task DeleteByExpression(
        Expression<Func<TEntity, bool>> deleteExpression,
        CancellationToken cancellationToken)
    {
        await Entities.Where(deleteExpression).ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<TEntity?> TryGetById(TKey entityId, CancellationToken cancellationToken)
    {
        var entity = await Entities.FindAsync(entityId);
        return entity;
    }
}