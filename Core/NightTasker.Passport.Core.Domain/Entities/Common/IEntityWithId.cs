namespace NightTasker.Passport.Domain.Entities.Common;

public interface IEntityWithId<T> : IEntity
{
    T Id { get; set; }
}