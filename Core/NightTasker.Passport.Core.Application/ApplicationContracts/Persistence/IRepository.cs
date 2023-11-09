namespace NightTasker.Passport.Application.ApplicationContracts.Persistence;

public interface IRepository<TEntity, TKey>
{
    Task<TEntity?> TryGetById(TKey entityId, CancellationToken cancellationToken);
}