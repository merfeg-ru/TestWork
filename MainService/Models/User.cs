﻿using CommonData;

namespace Sender.Models
{

    public class User : IUser
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
