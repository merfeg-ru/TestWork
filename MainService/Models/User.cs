using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainService.Models
{

    public class User : IRequest<bool>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string EMail { get; set; }

        public override string ToString()
        {
            return $"LastName:{LastName} FirstName:{FirstName} MiddleName:{MiddleName} PhoneNumber:{PhoneNumber} EMail:{EMail}";
        }
    }
}
