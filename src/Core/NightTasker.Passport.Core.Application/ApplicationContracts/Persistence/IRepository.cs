namespace NightTasker.Passport.Application.ApplicationContracts.Persistence;

/// <summary>
/// Репозиторий.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
/// <typeparam name="TKey">Тип ключа сущности.</typeparam>
public interface IRepository<TEntity, TKey>
{
    Task<TEntity?> TryGetById(TKey entityId, CancellationToken cancellationToken);
}