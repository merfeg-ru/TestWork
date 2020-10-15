using Microsoft.EntityFrameworkCore;
using ReceiverService.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ReceiverService.Context
{
    [ExcludeFromCodeCoverage]
    public class UsersContext : DbContext
    {
        public DbSet<UserDTO> Users { get; set; }
        public DbSet<OrganizationDTO> Organizations { get; set; }
        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UsersContextConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationContextConfiguration());
        }
    }
}
