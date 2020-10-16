using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceiverService.Models
{
    public class Organization
    {
        public int OrganizationId { get; set; }
        public string Name { get; set; }

        public List<User> Users { get; set; }
        public Organization()
        {
            Users = new List<User>();
        }
    }
}
