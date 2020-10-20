using System.Diagnostics.CodeAnalysis;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Receiver.Models;



namespace Receiver.Context
{
    [ExcludeFromCodeCoverage]
    public class UsersContextConfiguration : IEntityTypeConfiguration<UserDTO>
    {
        public void Configure(EntityTypeBuilder<UserDTO> entity)
        {
            entity.HasKey(e => new { e.UserId })
                .HasName("user_pkey");
        }
    }

    [ExcludeFromCodeCoverage]
    public class OrganizationContextConfiguration : IEntityTypeConfiguration<OrganizationDTO>
    {
        public void Configure(EntityTypeBuilder<OrganizationDTO> entity)
        {
            entity.HasKey(e => new { e.OrganizationId })
                .HasName("organization_pkey");
        }
    }
}
