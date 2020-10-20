using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Receiver.Models
{
    [Table("Organizations")]
    public class OrganizationDTO
    {
        public int OrganizationId { get; set; }
        public string Name { get; set; }

        public List<UserDTO> Users { get; set; }
        public OrganizationDTO()
        {
            Users = new List<UserDTO>();
        }
    }
}
