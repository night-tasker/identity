using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NightTasker.Identity.Domain.Entities.User;

namespace NightTasker.Identity.Infrastructure.Persistence.EntityConfigurations;

/// <summary>
/// Конфигурация сущности <see cref="Role"/>.
/// </summary>
public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(name: "roles");
    }
}