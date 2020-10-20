using System.ComponentModel.DataAnnotations.Schema;

namespace Receiver.Models
{
    [Table("Users")]
    public class UserDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string EMail { get; set; }
        public OrganizationDTO Organization { get; set; }
    }
}
