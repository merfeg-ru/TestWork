using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReceiverService.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ReceiverService.Context
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
