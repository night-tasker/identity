using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NightTasker.Identity.Domain.Entities.User;

namespace NightTasker.Identity.Infrastructure.Persistence.EntityConfigurations;

/// <summary>
/// Конфигурация сущности <see cref="UserRefreshToken"/>.
/// </summary>
public class UserRefreshTokenEntityConfiguration : IEntityTypeConfiguration<UserRefreshToken>
{
    public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        builder.ToTable(name: "user_refresh_tokens");
        
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder.Property(x => x.IsValid);

        builder.Property(x => x.CreatedDateTimeOffset);
    }
}