using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReceiverService.Models
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
