using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NightTasker.Identity.Domain.Entities.User;

namespace NightTasker.Identity.Infrastructure.Persistence.EntityConfigurations;

/// <summary>
/// Конфигурация сущности <see cref="UserLogin"/>.
/// </summary>
public class UserLoginEntityConfiguration : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder.ToTable(name: "user_logins");
    }
}