using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainService.Models
{

    public class User
    {
        [Required(ErrorMessage = "Не указано имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Не указан номер")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Неверно указан EMail")]
        public string EMail { get; set; }

        public override string ToString()
        {
            return $"LastName:{LastName} FirstName:{FirstName} MiddleName:{MiddleName} PhoneNumber:{PhoneNumber} EMail:{EMail}";
        }
    }
}
