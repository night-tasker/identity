namespace NightTasker.Identity.Domain.Entities.Common;

/// <summary>
/// Сущность с ИД.
/// </summary>
/// <typeparam name="T">Тип ИД.</typeparam>
public interface IEntityWithId<T> : IEntity
{
    /// <summary>
    /// ИД.
    /// </summary>
    T Id { get; set; }
}