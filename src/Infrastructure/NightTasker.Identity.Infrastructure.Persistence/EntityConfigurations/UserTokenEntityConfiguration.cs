using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NightTasker.Identity.Domain.Entities.User;

namespace NightTasker.Identity.Infrastructure.Persistence.EntityConfigurations;

/// <summary>
/// Конфигурация сущности <see cref="UserToken"/>.
/// </summary>
public class UserTokenEntityConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable(name: "user_tokens");
    }
}