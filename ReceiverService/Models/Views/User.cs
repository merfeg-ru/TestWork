﻿using CommonData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Receiver.Models
{
    public class User : IUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string EMail { get; set; }
    }
}
