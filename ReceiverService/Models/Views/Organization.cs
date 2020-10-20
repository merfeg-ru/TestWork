using System.Collections.Generic;

namespace Receiver.Models
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
