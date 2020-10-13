using Microsoft.EntityFrameworkCore;
using ReceiverService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceiverService.Context
{
    public class UsersContext : DbContext
    {
        public DbSet<UserDTO> Users { get; set; }
        public DbSet<OrganizationDTO> Organizations { get; set; }
        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
