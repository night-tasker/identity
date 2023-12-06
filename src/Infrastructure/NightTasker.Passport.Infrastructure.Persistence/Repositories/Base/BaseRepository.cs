using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NightTasker.Passport.Application.ApplicationContracts.Persistence;
using NightTasker.Passport.Domain.Entities.Common;

namespace NightTasker.Passport.Infrastructure.Repositories.Base;

/// <inheritdoc cref="IRepository{TEntity,TKey}"/>
internal abstract class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity
{
    /// <summary>
    /// Контекст для работы с данными.
    /// </summary>
    private readonly ApplicationDbContext _context;
    
    /// <summary>
    /// Записи определённой сущности.
    /// </summary>
    private DbSet<TEntity> Entities { get; init; }

    /// <summary>
    /// Таблица записей определённой сущности.
    /// </summary>
    protected virtual IQueryable<TEntity> Table => Entities;

    /// <summary>
    /// Неотслеживаемая таблица определённой сущности.
    /// </summary>
    protected virtual IQueryable<TEntity> NoTrackingTable => Entities.AsNoTrackingWithIdentityResolution();
    
    protected BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        Entities = context.Set<TEntity>();
    }
    
    /// <summary>
    /// Получить все записи.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список всех записей.</returns>
    protected virtual async Task<List<TEntity>> GetAll(CancellationToken cancellationToken)
    {
        return await Entities.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Добавить новую запись.
    /// </summary>
    /// <param name="entity">Запись.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    protected virtual async Task Add(TEntity entity, CancellationToken cancellationToken)
    { 
        await Entities.AddAsync(entity, cancellationToken);
    }

    /// <summary>
    /// Обновить записи, удовлетворяющие условию.
    /// </summary>
    /// <param name="updateExpression">Условие.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    protected virtual async Task UpdateByExpression(
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> updateExpression, 
        CancellationToken cancellationToken)
    {
        await Entities.ExecuteUpdateAsync(updateExpression, cancellationToken);
    }

    /// <summary>
    /// Удалить записи, удовлетворяющие условию.
    /// </summary>
    /// <param name="deleteExpression">Условие.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    protected virtual async Task DeleteByExpression(
        Expression<Func<TEntity, bool>> deleteExpression,
        CancellationToken cancellationToken)
    {
        await Entities.Where(deleteExpression).ExecuteDeleteAsync(cancellationToken);
    }

    /// <summary>
    /// Попробовать получить запись по ИД.
    /// </summary>
    /// <param name="entityId">ИД.</param>
    /// <param name="cancellationToken">Токен.</param>
    /// <returns>Запись.</returns>
    public async Task<TEntity?> TryGetById(TKey entityId, CancellationToken cancellationToken)
    {
        var entity = await Entities.FindAsync(entityId);
        return entity;
    }
}