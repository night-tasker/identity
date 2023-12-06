using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NightTasker.Passport.Domain.Entities.User;

namespace NightTasker.Passport.Infrastructure.EntityConfigurations;

/// <summary>
/// Конфигурация рефреш-токена пользователя.
/// </summary>
public class UserRefreshTokenEntityConfiguration : IEntityTypeConfiguration<UserRefreshToken>
{
    public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder.Property(x => x.IsValid);

        builder.Property(x => x.CreatedDateTimeOffset);
    }
}